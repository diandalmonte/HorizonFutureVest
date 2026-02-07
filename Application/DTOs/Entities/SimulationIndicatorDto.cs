using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Entidades;

namespace Application.DTOs.Entities
{
    public class SimulationIndicatorDto : BaseEntityDto<int>
    {
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool IsBetterHigh { get; set; }


        public required int MacroIndicatorId { get; set; }
        public MacroIndicator? MacroIndicator { get; set; }
    }
}
