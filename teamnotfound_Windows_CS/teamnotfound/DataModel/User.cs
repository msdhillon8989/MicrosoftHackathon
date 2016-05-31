using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNotFound.Models
{
    class User
    {
        private string id;
      
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        private string fname;
        [JsonProperty(PropertyName = "Fname")]
        public string Fname
        {
            get { return fname; }
            set { fname = value; }
        }

        private string lname;
        [JsonProperty(PropertyName = "Lname")]
        public string Lname
        {
            get { return lname; }
            set { lname = value; }
        }

        private string mobile;
        [JsonProperty(PropertyName = "Mobile")]
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }
        private string email;
        [JsonProperty(PropertyName = "Email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string summmary;
        [JsonProperty(PropertyName = "Summary")]
        public string Summary
        {
            get { return summmary; }
            set { summmary = value; }
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
