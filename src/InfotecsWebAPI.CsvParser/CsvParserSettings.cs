using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebApi.SepCsvParser
{
    public class CsvParserSettings
    {
        public bool UseHeaders { get; set; }
        public string DateFormat { get; set; } = string.Empty;
    }
}
