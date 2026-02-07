using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;
using Persistence.Entidades;

namespace Application.DTOs.Ranking
{
    public class NormalizedIndicatorDto
    {
        public required decimal NormalizedValue { get; set; }
        public required int Year { get; set; }


        public required int CountryId { get; set; }
        public Country? Country { get; set; }

        public required int MacroIndicatorId { get; set; }
        public MacroIndicator? MacroIndicator { get; set; }
    }
}
