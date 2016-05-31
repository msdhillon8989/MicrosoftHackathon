using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace teamnotfound.DataModel
{
    public class Project
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "bid")]
        public int Bid { get; set; }

        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
