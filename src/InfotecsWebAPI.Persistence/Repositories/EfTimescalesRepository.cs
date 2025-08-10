using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Abstractions.Repositories;
using InfotecsWebAPI.Application.Dtos.Filters;
using InfotecsWebAPI.Application.Exceptions;
using InfotecsWebAPI.Domain.Entities;
using InfotecsWebAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace InfotecsWebAPI.Persistence.Repositories
{
    internal class EfTimescalesRepository : ITimescaleRepository
    {
        private AppDbContext _dbContext;
        private DbSet<TimescaleFile> _files;
        private DbSet<TimescaleValues> _values;
        private DbSet<TimescaleResults> _results;

        public EfTimescalesRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _files = _dbContext.Files;
            _values = _dbContext.Values;
            _results = _dbContext.Results;
        }

        /// <summary>
        /// Add file info into database. If file already exists rewrites it.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        public async Task<TimescaleFile> AddFileAsync(TimescaleFile file)
        {
            var existingFile = await _files.FirstOrDefaultAsync(f => f.FileName == file.FileName);
            if (existingFile != null)
                _files.Remove(existingFile);

            try
            {
                await _files.AddAsync(file);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DatabaseException($"Could not update database: {ex.Message}");
            }

            return file;
        }

        /// <summary>
        /// Used to get TimescaleValues from database, that fit to filters.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<TimescaleValues>> GetFilteredTimescalesAsync(FileFilter filter)
        {

            // All files that fit filters
            var subquery = from f in _files
                           join r in _results on f.Id equals EF.Property<int>(r, "FileId")
                           where (
                               (string.IsNullOrEmpty(filter.FileName) || f.FileName == filter.FileName)
                               && (filter.FirstOperationTimeFrom == null || r.EarliestTime >= filter.FirstOperationTimeFrom.Value)
                               && (filter.FirstOperationTimeTo == null || r.EarliestTime <= filter.FirstOperationTimeTo.Value)
                               && (filter.AverageValueFrom == null || r.AverageValue >= filter.AverageValueFrom.Value)
                               && (filter.AverageValueTo == null || r.AverageValue <= filter.AverageValueTo.Value)
                               && (filter.AverageExecutionTimeFrom == null || r.AverageExecutionTime >= filter.AverageExecutionTimeFrom.Value)
                               && (filter.AverageExecutionTimeTo == null || r.AverageExecutionTime >= filter.AverageExecutionTimeTo.Value)
                           )
                           select f.Id;

            // all values from mentioned above files
            var query = from v in _values
                        where subquery.Contains(EF.Property<int>(v, "FileId"))
                        select v;

            if (filter.LastSortedByDateCount != null)
                query = query.OrderByDescending(d => d.Date).Take(filter.LastSortedByDateCount.Value);

            try
            {
                var result = await query.ToListAsync();
                return result;
            }
            catch (ArgumentException ex)
            {
                throw new DatabaseException($"{ex.Message} (Use 'Z' in the end of FirstOperationTimeFrom(To) string)");
            }
        }
    }
}
