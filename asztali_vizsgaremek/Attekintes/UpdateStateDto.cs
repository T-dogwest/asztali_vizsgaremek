using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Attekintes
{
    public class UpdateStateDto
    {

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReservationState State { get; set; }
    }
}
