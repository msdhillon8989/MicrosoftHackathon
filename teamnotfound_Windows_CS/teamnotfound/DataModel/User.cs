using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teamnotfound.DataModel
{
    class User
    {
        private string id;
      
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        

        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        
        private string email;
        [JsonProperty(PropertyName = "Email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string userType;
        [JsonProperty(PropertyName = "userType")]
        public string UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        /* [JsonProperty(PropertyName = "Image")]
         private byte[] image;
         public byte[] Image
         {
             get { return image; }
             set { image = value; }
         }
         */

    }
}
