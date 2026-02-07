using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.ReturnRate
{
    public class ReturnRateViewModel
    {
        [Required(ErrorMessage = "La tasa mínima es requerida")]
        [Display(Name = "Tasa de Retorno Mínima (%)")]
        public decimal MinReturnRate { get; set; }

        [Required(ErrorMessage = "La tasa máxima es requerida")]
        [Display(Name = "Tasa de Retorno Máxima (%)")]
        public decimal MaxReturnRate { get; set; }
    }
}
