using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.CountryIndicator
{
    public class SaveCountryIndicatorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El país es requerido")]
        public int CountryId { get; set; }
        [Required(ErrorMessage = "El indicador es requerido")]
        public int MacroIndicatorId { get; set; }
        [Required(ErrorMessage = "El valor es requerido")]
        public decimal Value { get; set; }
        [Required(ErrorMessage = "El año es requerido")]
        public int Year { get; set; }
    }
}
