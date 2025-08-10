using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Abstractions.Repositories;
using InfotecsWebAPI.Application.Common;
using InfotecsWebAPI.Application.Dtos.Filters;
using InfotecsWebAPI.Application.Exceptions;
using InfotecsWebAPI.Application.UseCases.Queries.GetFilteredTimescales;
using InfotecsWebAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InfotecsWebAPI.Application.UseCases.Queries.GetLastTimescales
{
    /// <summary>
    /// Use this to get last COUNT values of the file named FILE
    /// </summary>
    public class GetLastTimescalesQueryHandler : IRequestHandler<GetLastTimescalesQuery, Response<List<TimescaleValues>>>
    {
        ITimescaleRepository _repository;
        ILogger<GetFilteredTimescalesQueryHandler> _logger;

        public GetLastTimescalesQueryHandler(ITimescaleRepository repository, ILogger<GetFilteredTimescalesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<TimescaleValues>>> Handle(GetLastTimescalesQuery request, CancellationToken cancellationToken)
        {
            var filters = new FileFilter
            {
                FileName = request.FileName,
                LastSortedByDateCount = request.Count
            };

            List<TimescaleValues> records;
            try
            {
                records = await _repository.GetFilteredTimescalesAsync(filters);
            }
            catch (DatabaseConnectionException ex)
            {
                return Response<List<TimescaleValues>>.Failure($"Could not query data from database: {ex.Message}");
            }

            return Response<List<TimescaleValues>>.Success(records);
        }
    }
}
