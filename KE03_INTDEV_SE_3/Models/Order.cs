using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KE03_INTDEV_SE_3.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<Product>? Products { get; set; } 
        public List<DeliveryState>? DeliveryStates { get; set; }

        public string LatestStatus
        {
            get
            {
                if (DeliveryStates == null || !DeliveryStates.Any())
                    return "Nog geen status";

                var laatste = DeliveryStates
                    .Where(ds => ds != null)
                    .OrderByDescending(x => x.DateTime)
                    .FirstOrDefault();

                return laatste != null
                    ? GetEnumDisplayName(laatste.State)
                    : "Geen geldige status";
            }
        }

        private string GetEnumDisplayName(Enum enumValue)
        {
            var displayAttribute = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }
}
