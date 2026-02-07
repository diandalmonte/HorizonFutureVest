using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entidades
{
    public class SimulationIndicator : BaseEntity<int>, IMacroIndicator
    {
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool IsBetterHigh { get; set; }


        public required int MacroIndicatorId { get; set; } //FK para eliminacion en cascada en caso de que el tipo de MacroIndicador sea eliminado del sistema
        public MacroIndicator? MacroIndicator { get; set; }
    }
}
