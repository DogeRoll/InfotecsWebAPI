using System.Text;
using System.Text.Json;
using InfotecsWebAPI.Application.Common;
using InfotecsWebAPI.Application.Dtos.Filters;
using InfotecsWebAPI.Application.Dtos.Queries;
using InfotecsWebAPI.Application.UseCases.Commands.FileUpload;
using InfotecsWebAPI.Application.UseCases.Queries.GetFilteredTimescales;
using InfotecsWebAPI.Application.UseCases.Queries.GetLastTimescales;
using InfotecsWebAPI.Domain.Entities;
using InfotecsWebAPI.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InfotecsWebAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimescalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TimescalesController> _logger;

        public TimescalesController(IMediator mediator, ILogger<TimescalesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            var content = await FileReader.ReadAsStringAsync(file);
            if (content == null)
                return BadRequest("Bad file");

            var command = new FileUploadCommand
            {
                Filename = file.FileName,
                Content = content
            };

            _logger.LogInformation("Executed upload api");

            return Ok(await _mediator.Send(command));
        }


        [HttpGet("get-filtered")]
        public async Task<ActionResult<Response<IEnumerable<TimescaleValues>>>> GetFilteredRecords([FromQuery] GetFilteredTimescalesQuery query)
        {
            _logger.LogInformation(JsonSerializer.Serialize(query));
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("get-last")]
        public async Task<ActionResult<Response<IEnumerable<TimescaleValues>>>> GetLastRecords([FromQuery] LastTimescalesDto query)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid query parameters");

            var mquery = new GetLastTimescalesQuery
            {
                FileName = query.FileName,
                Count = query.Count
            };

            _logger.LogInformation(JsonSerializer.Serialize(mquery));
            return Ok(await _mediator.Send(mquery));
        }
    }
}
