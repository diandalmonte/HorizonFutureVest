using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entidades
{
    public class SimulationSettings : BaseEntity<int>
    {
        public List<MacroIndicator> MacroIndicators { get; set; } = new List<MacroIndicator>();
    }
}
