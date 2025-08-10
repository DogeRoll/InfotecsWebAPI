using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Dtos.Filters
{
    public class FileFilter
    {
        public string? FileName { get; set; }
        public DateTime? FirstOperationTimeFrom { get; set; }
        public DateTime? FirstOperationTimeTo { get; set; }
        public float? AverageValueFrom { get; set; }
        public float? AverageValueTo { get; set; }
        public float? AverageExecutionTimeFrom { get; set; }
        public float? AverageExecutionTimeTo { get; set; }
        public int? LastSortedByDateCount { get; set; }
    }
}
