using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static asztali_vizsgaremek.MenuItem;

namespace asztali_vizsgaremek.User
{
    public class FelhasznmalokItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("first_name")]
        public string First_name { get; set; }

        [JsonProperty("last_name")]
        public string Last_name { get; set; }



        [JsonProperty("role")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RoleType Role { get; set; }
    }


    public enum RoleType { Admin, User }

}
