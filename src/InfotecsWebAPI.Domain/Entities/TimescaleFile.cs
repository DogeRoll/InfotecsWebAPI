using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Domain.Entities
{
    public class TimescaleFile : EntityBase
    {
        public string FileName { get; set; }
        
        public TimescaleResults Results { get; set; }
        public List<TimescaleValues> Values { get; set; }
    }
}
