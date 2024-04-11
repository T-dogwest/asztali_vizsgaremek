using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Velemenyekk
{ /// <summary>
  /// A vélemények adatátvitelére használt adattranszfer objektum.
  /// </summary>
    class VelemenyekDTO
    {
        /// <summary>
        /// A vélemények adatátvitelére használt adattranszfer objektum.
        /// </summary>
        [JsonProperty("rate")]
        public int Rate { get; set; }
        /// <summary>
        /// A vélemény tartalma.
        /// </summary>

        [JsonProperty("content")]
        public string Content { get; set; }
        /// <summary>
        /// A véleményt beküldő felhasználó felhasználóneve.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

    }
}
