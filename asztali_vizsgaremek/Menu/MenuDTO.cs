using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace asztali_vizsgaremek.Menu
{
    /// <summary>
    /// Az étel vagy ital menüelem létrehozásához használt adattranszfer objektum (DTO).
    /// </summary>
    internal class MenuDTO
    {
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
