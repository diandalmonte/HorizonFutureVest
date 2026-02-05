using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entidades
{
    public class MacroIndicator : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required decimal Weight { get; set; }
        public required bool isBetterHigh { get; set; }
    }
}
