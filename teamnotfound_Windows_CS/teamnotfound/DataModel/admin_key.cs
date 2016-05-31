using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{
    class admin_key
    {
        public string Id { get; set; }
        [JsonProperty(PropertyName = "key")]
        public string Key { set; get; }
    }
}
