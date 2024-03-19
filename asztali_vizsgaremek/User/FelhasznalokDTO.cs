using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    class FelhasznalokDTO
    {
        [JsonProperty(" username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("first_name")]
        public string First_name { get; set; }
        [JsonProperty("last_name")]
        public string Last_name { get; set; }



        [JsonProperty("roleType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FelhasznalokItem.RoleType RoleType { get; set; }
    }


    

}

