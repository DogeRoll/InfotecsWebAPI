using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfotecsWebAPI.Application.Common;
using InfotecsWebAPI.Domain.Entities;
using MediatR;

namespace InfotecsWebAPI.Application.UseCases.Queries.GetLastTimescales
{
    public class GetLastTimescalesQuery : IRequest<Response<List<TimescaleValues>>>
    {
        public string? FileName { get;set; }
        public int Count { get; set; }
    }
}
