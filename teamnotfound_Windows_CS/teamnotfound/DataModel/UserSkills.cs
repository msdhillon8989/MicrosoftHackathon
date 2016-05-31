using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{
    public class UserSkills
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "emailId")]
        public string EmailId { get; set; }

        [JsonProperty(PropertyName = "sid")]
        public int Sid { get; set; }
    }
}
