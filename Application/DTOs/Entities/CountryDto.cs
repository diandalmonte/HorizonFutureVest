using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entities
{
    public class CountryDto : BaseEntityDto<int>
    {
        public required string Name { get; set; }
        public required string IsoCode { get; set; }
    }
}
