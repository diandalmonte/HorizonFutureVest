using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ranking
{
    public class RankingResultDto
    {
        public string CountryName { get; set; }
        public string CountryIsoCode { get; set; }
        public decimal Score { get; set; } //CHECK si decimal termina siendo el data type correcto
        public decimal ReturnRate { get; set; } //CHECK si decimal termina siendo el data type correcto
    }
}
