using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Abstractions.CsvParser;
using InfotecsWebAPI.Application.Abstractions.Repositories;
using InfotecsWebAPI.Application.Common;
using InfotecsWebAPI.Application.Dtos.CsvParsing;
using InfotecsWebAPI.Application.Dtos.Filters;
using InfotecsWebAPI.Application.Exceptions;
using InfotecsWebAPI.Application.UseCases.Commands.FileUpload;
using InfotecsWebAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InfotecsWebAPI.Application.UseCases.Queries.GetFilteredTimescales
{
    public class GetFilteredTimescalesQuery : IRequest<Response<List<TimescaleValues>>>
    {
        public string? FileName { get; set; }
        public DateTime? FirstOperationTimeFrom { get; set; }
        public DateTime? FirstOperationTimeTo { get; set; }
        public float? AverageValueFrom { get; set; }
        public float? AverageValueTo { get; set; }
        public float? AverageExecutionTimeFrom { get; set; }
        public float? AverageExecutionTimeTo { get; set; }
    }
}
