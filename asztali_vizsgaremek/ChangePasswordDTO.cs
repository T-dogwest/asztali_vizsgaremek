using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    internal class ChangePasswordDTO
    {
        [JsonProperty("oldpass")]
        public string OldPassword { get; set; }
        [JsonProperty("newpass")]
        public string NewPassword { get; set; }
    }
}
