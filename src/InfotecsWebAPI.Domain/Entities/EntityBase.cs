using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Domain.Entities
{
    /// <summary>
    /// Idk why i use it in single TimescaleFile class
    /// </summary>
    public abstract class EntityBase
    {
        public int Id { get; set; }
    }
}
