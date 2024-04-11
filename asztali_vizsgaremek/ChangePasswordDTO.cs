using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// Az új jelszó beállításához használt adattranszfer objektum.
    /// </summary>
    internal class ChangePasswordDTO
    {
        /// <summary>
        /// A régi jelszó.
        /// </summary>
        [JsonProperty("oldpass")]
        public string OldPassword { get; set; }
        /// <summary>
        /// Az új jelszó.
        /// </summary>
        [JsonProperty("newpass")]
        public string NewPassword { get; set; }
    }
}
