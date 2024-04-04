using Newtonsoft.Json;
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
        public ReservationState State { get; set; }
    }
}
