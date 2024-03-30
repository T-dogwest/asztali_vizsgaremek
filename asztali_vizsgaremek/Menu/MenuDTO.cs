using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace asztali_vizsgaremek.Menu
{
    internal class MenuDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MenuType ItemType { get; set; }
    }
}
