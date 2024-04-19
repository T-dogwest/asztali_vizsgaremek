using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Nyitvatartas
{ /// <summary>
  /// Az üzlet nyitvatartását reprezentáló osztály.
  /// </summary>
    class OpeningItem
    {
        /// <summary>
        /// Az üzlet nyitvatartásának azonosítója.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Az üzlet nyitvatartása hétfőn.
        /// </summary>
        [JsonProperty("monday")]
        public string Monday { get; set; }
        /// <summary>
        /// Az üzlet nyitvatartása kedden.
        /// </summary>
        [JsonProperty("tuesday")]
        public string Tuesday { get; set; }
        /// <summary>
        /// Az üzlet nyitvatartása szerdán.
        /// </summary>
        [JsonProperty("wednesday")]
        public string Wednesday { get; set; }

        /// <summary>
        /// Az üzlet nyitvatartása csütörtökön.
        /// </summary>
        [JsonProperty("thursday")]
        public string Thursday { get; set; }
        /// <summary>
        /// Az üzlet nyitvatartása pénteken.
        /// </summary>
        [JsonProperty("friday")]
        public string Friday { get; set; }
        /// <summary>
        /// Az üzlet nyitvatartása szombaton.
        /// </summary>
        [JsonProperty("saturday")]
        public string Saturday { get; set; }
        /// <summary>
        /// Az üzlet nyitvatartása vasárnap.
        /// </summary>
        [JsonProperty("sunday")]
        public string Sunday { get; set; }

    }
}
