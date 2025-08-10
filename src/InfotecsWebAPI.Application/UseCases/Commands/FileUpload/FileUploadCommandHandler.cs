using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using InfotecsWebAPI.Application.Abstractions.CsvParser;
using InfotecsWebAPI.Application.Abstractions.Repositories;
using InfotecsWebAPI.Application.Common;
using InfotecsWebAPI.Application.Dtos.CsvParsing;
using InfotecsWebAPI.Application.Exceptions;
using InfotecsWebAPI.Application.Services;
using InfotecsWebAPI.Application.UseCases.Commands.FileUpload;
using InfotecsWebAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Wintellect.PowerCollections;

namespace InfotecsWebAPI.Application.UseCases.Commands.FileUpload
{
    public class FileUploadCommandHandler
        : IRequestHandler<FileUploadCommand, Result>
    {
        private readonly ITimescaleRepository _timescaleRepository;
        private readonly ICsvParser<TimescaleDto> _parser;
        private readonly ILogger<FileUploadCommandHandler> _logger;

        private const int _maxLines = 10000;
        private const int _minLines = 1;

        public FileUploadCommandHandler(ITimescaleRepository repository, ICsvParser<TimescaleDto> parser, ILogger<FileUploadCommandHandler> logger)
        {
            _timescaleRepository = repository;
            _parser = parser;
            _logger = logger;
        }

        /// <summary>
        /// Parses csv string and either add it into database or return failure if it is not valid.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> Handle(FileUploadCommand request, CancellationToken cancellationToken)
        {
            var linesCount = request.Content.Count(c => c == '\n');
            if (linesCount < _minLines || linesCount > _maxLines)
                return Result.Failure($"The file contains {linesCount} lines. Acceptable range: [{_minLines}, {_maxLines}].");

            List<TimescaleValues> tsdata = new();
            try
            {
                var validator = new TimescaleDtoValidator();
                foreach (var item in _parser.Parse(request.Content))
                {
                    validator.ValidateAndThrow(item);
                    tsdata.Add(new TimescaleValues(item.Date, item.ExecutionTime, item.Value));
                }
            }
            catch (Exception ex)
            {
                return Result.Failure($"Error on reading file: {ex.Message}");
            }

            var results = TimescaleResults.FromValues(tsdata);

            var file = new TimescaleFile
            {
                FileName = request.Filename,
                Values = tsdata,
                Results = results
            };

            try
            {
                await _timescaleRepository.AddFileAsync(file);
            }
            catch (DatabaseException ex)
            {
                return Result.Failure($"Database error: {ex.Message}");
            }

            return Result.Success();
        }
    }
}
