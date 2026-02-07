using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Common
{
    public interface IMacroIndicator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public bool IsBetterHigh { get; set; }
    }
}
