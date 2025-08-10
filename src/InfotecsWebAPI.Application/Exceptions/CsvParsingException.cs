using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Exceptions
{
    public class CsvParsingException(string message) : Exception(message)
    {
    }
}
