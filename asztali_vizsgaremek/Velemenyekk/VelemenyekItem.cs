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
    /// <summary>
    /// A véleményeket reprezentáló osztály.
    /// </summary>
    class VelemenyekItem
    {
        /// <summary>
        /// A vélemény egyedi azonosítója.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// A vélemény értékelése.
        /// </summary>
        [JsonProperty("rate")]
        public int Rate { get; set; }
        /// <summary>
        /// A vélemény tartalma.
        /// </summary>

        [JsonProperty("content")]
        public string Content { get; set; }
        /// <summary>
        /// A véleményt beküldő felhasználó azonosítója.
        /// </summary>

        [JsonProperty("userId")]
        public int UserId { get; set; }
        /// <summary>
        /// A véleményt beküldő felhasználó.
        /// </summary>

        [JsonProperty("user")]
        public User User { get; set; }
        /// <summary>
        /// A véleményt beküldő felhasználó felhasználóneve.
        /// </summary>
        public string UserName
        {
            get { return User?.Username; } 
        }
    }

    /// <summary>
    /// A felhasználót reprezentáló osztály.
    /// </summary>
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}



  

