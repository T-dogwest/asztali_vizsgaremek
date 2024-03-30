using Azure.Identity;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Velemenyekk
{
    class VelemenyekItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("rate")]
        public int Rate { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        public string UserName
        {
            get { return User?.Username; } 
        }
    }

    // Felhasználó osztály
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}



  

