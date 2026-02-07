using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;

namespace Application.DTOs.Filters
{
    public class CountryIndicatorFilter
    {
        public int? Year { get; set; }
        public int? MacroIndicatorId { get; set; }
    }
}
