using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Abstractions.CsvParser
{
    public interface ICsvParser<TCsvDto>
    {
        IEnumerable<TCsvDto> Parse(string csvString);

    }
}
