using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;

namespace Application.DTOs.Entities
{
    public class CountryIndicatorDto : BaseEntityDto<int>
    {
        public required decimal Value { get; set; }
        public required int Year { get; set; }


        public required int CountryId { get; set; }
        public Country? Country { get; set; }

        public required int MacroIndicatorId { get; set; }
        public MacroIndicator? MacroIndicator { get; set; }
    }
}
