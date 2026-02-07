using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InvalidReturnRateException : Exception
    {
        public InvalidReturnRateException() { }

        public InvalidReturnRateException(string message) : base(message) { }
    }
}
