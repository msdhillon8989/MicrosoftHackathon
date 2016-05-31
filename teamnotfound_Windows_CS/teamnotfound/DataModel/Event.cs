using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{
    public class Event
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { set; get; }
        [JsonProperty(PropertyName = "start_date")]
        public DateTime StartDate { set; get; }
        [JsonProperty(PropertyName = "end_date")]
        public DateTime EndDate { set; get; }
        [JsonProperty(PropertyName = "base_price")]
        public int BasePrice { get; set; }
        [JsonProperty(PropertyName = "location")]
        public string Location { get; set; }
        [JsonProperty(PropertyName = "closeDate")]
        public DateTime CloseDate { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}
