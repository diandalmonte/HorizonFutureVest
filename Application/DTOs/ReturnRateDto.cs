using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ReturnRateDto
    {
        public required decimal MinReturnRate { get; set; }
        public required decimal MaxReturnRate { get; set; }
    }
}
