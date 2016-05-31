using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{

    
   public  class Bid

    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "project_id")]
        public string ProjectId { set; get; }
        [JsonProperty(PropertyName = "bidder")]
        public string Bidder { set; get; }
        [JsonProperty(PropertyName = "bidd_amt")]
        public int BiddAmt { set; get; }
        [JsonProperty(PropertyName = "TimePeriod")]
        public int TimePeriod { get; set; }
    }
}
