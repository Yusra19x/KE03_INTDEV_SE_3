using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KE03_INTDEV_SE_3.Models
{
    public enum DeliveryStateEnum
    {
        [Display(Name = "In behandeling")]
        InBehandeling = 1,

        Onderweg = 2,

        Bezorgd = 3,

        Geannuleerd = 4
    }
}
