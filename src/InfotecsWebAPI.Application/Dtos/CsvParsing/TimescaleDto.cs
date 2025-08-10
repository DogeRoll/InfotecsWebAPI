using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Dtos.CsvParsing
{
    // is sending from parser infrastructure to application
    public class TimescaleDto
    {
        public DateTime Date { get; set; }
        public uint ExecutionTime { get; set; }
        public float Value { get; set; }
    }
}
