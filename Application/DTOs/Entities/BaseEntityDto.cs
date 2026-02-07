using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entities
{
    public class BaseEntityDto<TId>
    {
        public TId? Id { get; set; }
    }
}
