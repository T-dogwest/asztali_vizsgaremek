﻿using asztali_vizsgaremek.Menu;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Attekintes
{
    public class AttekintesItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }
      

        [JsonProperty("bicycle_id")]
        public int BicycleId { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("reservation_time")]
        public string ReservationTime { get; set; }

        [JsonProperty("state")]
        public ReservationState State { get; set; }

        [JsonProperty("total_amount")]
        public int TotalAmount { get; set; }

        [JsonProperty("user")]
        public User UserData { get; set; }

        [JsonProperty("bicycle")]
        public Bicycle Bicycle { get; set; }

        [JsonProperty("basket")]
        public List<Menu> Basket { get; set; }


        public string UserName
        {
            get { return UserData?.Username; }
        }
        public string UserEmail
        {
            get { return UserData?.Email; }
        }


        public BicycleType? BicycleType
        {
            get { return Bicycle?.Type; }
        }
    }

    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
    public class Menu
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public MenuType Type { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }
    }



    public class Bicycle
    {
        [JsonProperty("type")]
        public BicycleType Type { get; set; }
    }

    public enum ReservationState
    {
        Cancelled,
        Pending,
        Done
    }

    public enum BicycleType
    {
        Small,
        Medium,
        Large
    }
    public enum MenuType
    {
        Drink,
        Snack
    }
}

