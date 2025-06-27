using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KE03_INTDEV_SE_3.Models
{
    public class DeliveryState
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int DeliveryServiceId { get; set; }
        public DeliveryService? DeliveryService { get; set; }
        public DeliveryStateEnum State { get; set; }

    }
}
    public enum DeliveryStateEnum
    {
        Created = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }


