using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Nyitvatartas
{
    class OpeningItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("monday")]
        public string Monday { get; set; }
        [JsonProperty("tuesday")]
        public string Tuesday { get; set; }
        [JsonProperty("wednesday")]
        public string Wednesday { get; set; }
        [JsonProperty("thursday")]
        public string Thursday { get; set; }
        [JsonProperty("friday")]
        public string Friday { get; set; }
        [JsonProperty("sasturday")]
        public string Sasturday { get; set; }
        [JsonProperty("sunday")]
        public string Sunday { get; set; }

    }
}
