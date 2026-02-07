using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entities
{
    public class MacroIndicatorDto : BaseEntityDto<int>
    {
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool IsBetterHigh { get; set; }
    }
}
