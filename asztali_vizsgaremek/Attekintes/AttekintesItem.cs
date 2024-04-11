using asztali_vizsgaremek.Menu;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Attekintes
{   /// <summary>
    /// Egy osztály, amely egy foglalás áttekintő elemét reprezentálja.
    /// </summary>
    public class AttekintesItem
    {
        /// <summary>
        /// Az elem egyedi azonosítóját adja vagy állítja be.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Azon felhasználó azonosítója, aki a foglalást létrehozta.
        /// </summary>
        [JsonProperty("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// A bicikli azonosítója, amelyet foglaltak.
        /// </summary>
        [JsonProperty("bicycle_id")]
        public int BicycleId { get; set; }
        /// <summary>
        /// A foglalás kezdési ideje.
        /// </summary>
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
        /// <summary>
        /// A foglalás helyszíne.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }
        /// <summary>
        /// A foglalás ideje.
        /// </summary>
        [JsonProperty("reservation_time")]
        public string ReservationTime { get; set; }
        /// <summary>
        /// A foglalás állapota
        /// </summary>

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReservationState State { get; set; }
        /// <summary>
        /// A kosár azonosítója, amelyhez a foglalás tartozik.
        /// </summary>
        [JsonProperty("basket_id")]
        public int BasketId { get; set; }
        /// <summary>
        /// A kosár elem, amelyhez a foglalás tartozik.
        /// </summary>
        [JsonProperty("basket")]
        public BasketItem Basket { get; set; }
        /// <summary>
        /// A foglalás teljes összege.
        /// </summary>
        [JsonProperty("total_amount")]
        public int TotalAmount { get; set; }
        /// <summary>
        /// A felhasználó adatait tartalmazó osztály.
        /// </summary>
        [JsonProperty("user")]
        public User UserData { get; set; }
        /// <summary>
        /// A bicikli adatait tartalmazó osztály.
        /// </summary>
        [JsonProperty("bicycle")]
        public Bicycle Bicycle { get; set; }



        /// <summary>
        /// Visszaadja a felhasználó nevét.
        /// </summary>
        public string UserName
        {
            get { return UserData?.Username; }
        }
        /// <summary>
        /// Visszaadja a felhasználó e-mail címét.
        /// </summary>
        public string UserEmail
        {
            get { return UserData?.Email; }
        }

        /// <summary>
        /// Visszaadja a bicikli típusát.
        /// </summary>
        public BicycleType? BicycleType
        {
            get { return Bicycle?.Type; }
        }
    }

    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
      
    }
    /// <summary>
    /// Egy kosarat reprezentáló osztály.
    /// </summary>
    public class BasketItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("deletedAt")]
        public object DeletedAt { get; set; }

        [JsonProperty("menu")]
        public List<MenuAttekintes> Menu { get; set; }
    }
    /// <summary>
    /// Az egyes menüelemeket a kosárban reprezentáló osztály.
    /// </summary>
    public class MenuAttekintes
    {
        [JsonProperty("name")]
        public string NameKosar { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }



    public class Bicycle
    {
        [JsonProperty("type")]
        public BicycleType Type { get; set; }
    }
    /// <summary>
    /// Az egyes foglalások állapotait reprezentáló felsorolás.
    /// </summary>
    public enum ReservationState
    {
        Cancelled,
        Pending,
        Done
    }
    /// <summary>
    /// Az egyes bicikli típusokat reprezentáló felsorolás.
    /// </summary>
    public enum BicycleType
    {
        Small,
        Medium,
        Large
    }
  
}

