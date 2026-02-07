using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.SimulationIndicator
{
    public class SimulationIndicatorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public bool IsBetterHigh { get; set; }
        public string MacroIndicatorName { get; set; }
    }
}
