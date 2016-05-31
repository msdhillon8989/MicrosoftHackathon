using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{
    class Country
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "country_name")]
        public string CountryName { set; get; }
    }
}
