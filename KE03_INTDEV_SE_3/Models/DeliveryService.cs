﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace KE03_INTDEV_SE_3.Models
{
    public class DeliveryService
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("apiKey")]
        public string? ApiKey { get; set; }
    }
}
