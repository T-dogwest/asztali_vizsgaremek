using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Menu
{    /// <summary>
     /// Az étel vagy ital menüelemet reprezentáló osztály.
     /// </summary>
    public class MenuItem
    { /// <summary>
      /// Az elem azonosítója.
      /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }
        /// <summary>
        /// Az elem neve.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Az elem ára.
        /// </summary>
        [JsonProperty("price")]
        public int Price { get; set; }
        /// <summary>
        /// Az elem típusa (étel vagy ital).
        /// </summary>
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MenuType ItemType { get; set; }
    }
}
