using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.MacroIndicator
{
    public class SaveMacroIndicatorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El peso es requerido")]
        public decimal Weight { get; set; }
        public bool IsBetterHigh { get; set; }
    }
}
