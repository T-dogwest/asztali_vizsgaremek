using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Velemenyekk
{
    /// <summary>
    /// A vélemények kezeléséért felelős szolgáltatásosztály.
    /// </summary>
    internal class VelemenyekServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/Review";
        private string url1 = "http://localhost:3000/Review/AdminRevDelete";

        /// <summary>
        /// A VelemenyekServices osztály konstruktora.
        /// Beállítja az alapértelmezett HTTP kérések fejlécét az autentikációs tokennel.
        /// </summary>
        public VelemenyekServices()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }
        /// <summary>
        /// Az összes vélemény lekérdezését végzi a szerverről.
        /// </summary>
        /// <returns>A lekért vélemények listája.</returns>

        public List<VelemenyekItem> GetAll()
        {
            try
            {
                string json = client.GetStringAsync(url).Result;
                Debug.WriteLine(json);
                return JsonConvert.DeserializeObject<List<VelemenyekItem>>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba történt a Get kérés során: {ex.Message}");
                return null;
            }
        }
        /// <summary>
        /// Törli a megadott véleményt a szerverről.
        /// </summary>
        /// <param name="velemeny">A törlendő vélemény.</param>
        /// <returns>Igaz, ha a törlés sikeres, egyébként hamis.</returns>

        public bool Delete(VelemenyekItem velemeny)
        {
            int id = velemeny.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url1}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
