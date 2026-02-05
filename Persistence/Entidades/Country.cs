using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Persistence.Common;

namespace Persistence.Entidades
{
    public class Country : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string IsoCode { get; set; }
    }
}
