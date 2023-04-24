using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanAPI.Application.Exceptions
{
    public class InternalServerException : ApplicationException
    {
        public InternalServerException(string message) : base(message)
        { }
    }
}
