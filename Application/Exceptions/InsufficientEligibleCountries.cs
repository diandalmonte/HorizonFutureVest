using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class InsufficientEligibleCountries : Exception
    {
        public InsufficientEligibleCountries() { }

        public InsufficientEligibleCountries(string message) : base(message) { }
    }
}
