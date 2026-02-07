using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CountryIndicator
{
    public class CountryIndicatorViewModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string MacroIndicatorName { get; set; }
        public decimal Value { get; set; }
        public int Year { get; set; }
    }
}
