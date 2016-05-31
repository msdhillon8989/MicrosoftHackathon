using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{
    class Skills
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "sid")]
        public string Sid { get; set; }

        [JsonProperty(PropertyName = "skill")]
        public string Skill { get; set; }
    }
}
