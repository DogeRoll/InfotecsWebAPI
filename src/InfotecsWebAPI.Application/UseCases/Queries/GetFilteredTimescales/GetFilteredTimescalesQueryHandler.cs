using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Abstractions.Repositories;
using InfotecsWebAPI.Application.Common;
using InfotecsWebAPI.Application.Dtos.Filters;
using InfotecsWebAPI.Application.Exceptions;
using InfotecsWebAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InfotecsWebAPI.Application.UseCases.Queries.GetFilteredTimescales
{
    public class GetFilteredTimescalesQueryHandler : IRequestHandler<GetFilteredTimescalesQuery, Response<List<TimescaleValues>>>
    {
        ITimescaleRepository _repository;
        ILogger<GetFilteredTimescalesQueryHandler> _logger;

        public GetFilteredTimescalesQueryHandler(ITimescaleRepository repository, ILogger<GetFilteredTimescalesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<TimescaleValues>>> Handle(GetFilteredTimescalesQuery request, CancellationToken cancellationToken)
        {
            var filters = new FileFilter
            {
                FileName = request.FileName,
                FirstOperationTimeFrom = request.FirstOperationTimeFrom,
                AverageValueFrom = request.AverageValueFrom,
                AverageExecutionTimeFrom = request.AverageExecutionTimeFrom,
                FirstOperationTimeTo = request.FirstOperationTimeTo,
                AverageValueTo = request.AverageValueTo,
                AverageExecutionTimeTo = request.AverageExecutionTimeTo,
                LastSortedByDateCount = null
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
