using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.User
{  /// <summary>
   /// A felhasználó adatainak frissítésére használt adattranszfer objektum.
   /// </summary>
    public class UpdateFelhasznaloDTO
    {
        /// <summary>
        /// A felhasználó egyedi azonosítója.
        /// </summary>
        [JsonProperty("id")] 
        public int Id { get; set; }
        /// <summary>
        /// A felhasználó felhasználóneve.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }
        /// <summary>
        /// A felhasználó keresztneve.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// A felhasználó vezetékneve.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// A felhasználó e-mail címe.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
