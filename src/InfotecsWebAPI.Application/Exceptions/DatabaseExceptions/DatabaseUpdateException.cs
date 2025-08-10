using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfotecsWebAPI.Application.Exceptions
{
    internal class DatabaseUpdateException(string message) : DatabaseException(message)
    {
    }
}
