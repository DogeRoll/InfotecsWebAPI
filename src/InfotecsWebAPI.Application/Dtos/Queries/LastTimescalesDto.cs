using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Dtos.Queries
{
    public class LastTimescalesDto
    {
        [Required]
        public string? FileName { get; set; }

        [Range(0, int.MaxValue)]
        public int Count { get; set; }
    }
}
