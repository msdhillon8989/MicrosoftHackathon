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
        [JsonProperty(PropertyName = "eventId")]
        public string EventId { set; get; }
        [JsonProperty(PropertyName = "bidder")]
        public string Bidder { set; get; }
        [JsonProperty(PropertyName = "bidd_amt")]
        public int BiddAmt { set; get; }
        [JsonProperty(PropertyName = "country")]
        public String Countr { get; set; }
<<<<<<< HEAD
        /*[JsonProperty(PropertyName = "status")]
        public String Status { get; set; }
        */
=======
        [JsonProperty(PropertyName = "status")]
        public String Status { get; set; }


>>>>>>> origin/master
    }
}
