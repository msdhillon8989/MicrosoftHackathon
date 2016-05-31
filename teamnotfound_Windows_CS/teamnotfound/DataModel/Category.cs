using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace teamnotfound.DataModel
{
    public class Category
    {
        public int count { set; get; }
        public string Id { set; get; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { set; get; }
    }
}
