using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Dtos.Filters;
using InfotecsWebAPI.Domain.Entities;

namespace InfotecsWebAPI.Application.Abstractions.Repositories
{
    public interface ITimescaleRepository
    {
        public Task<List<TimescaleValues>> GetFilteredTimescalesAsync(FileFilter filter);
        public Task<TimescaleFile> AddFileAsync(TimescaleFile file);
    }
}
