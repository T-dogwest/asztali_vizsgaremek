using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace asztali_vizsgaremek.User
{/// <summary>
 /// Egy felhasználóhoz tartozó adatmodell.
 /// </summary>
    public class FelhasznmalokItem
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
        /// A felhasználó e-mail címe.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// A felhasználó jelszava.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
        /// <summary>
        /// A felhasználó keresztneve.
        /// </summary>
        [JsonProperty("first_name")]
        public string First_name { get; set; }
        /// <summary>
        /// A felhasználó vezetékneve.
        /// </summary>
        [JsonProperty("last_name")]
        public string Last_name { get; set; }


        /// <summary>
        /// A felhasználó szerepe.
        /// </summary>
        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleType Role { get; set; }
    }

    /// <summary>
    /// A felhasználó szerepét definiáló felsorolási típus.
    /// </summary>
    public enum RoleType { Admin, User }

}
