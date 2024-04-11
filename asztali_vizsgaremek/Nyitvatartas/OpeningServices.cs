using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Nyitvatartas
{ /// <summary>
  /// Az üzlet nyitvatartását kezelő szolgáltatásokat biztosító osztály.
  /// </summary>
    internal class OpeningServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/opening";
        /// <summary>
        /// Az OpeningServices osztály konstruktora.
        /// </summary>
        public OpeningServices() 
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }
        /// <summary>
        /// Az összes nyitvatartási adat lekérdezése.
        /// </summary>
        /// <returns>Az összes nyitvatartási adatot tartalmazó lista</returns>
        public List<OpeningItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<OpeningItem>>(json);
        }
        /// <summary>
        /// Egy nyitvatartási adat módosítása.
        /// </summary>
        /// <param name="id">A módosítandó nyitvatartási adat azonosítója</param>
        /// <param name="opening">Az új nyitvatartási adat</param>
        /// <returns>A frissített nyitvatartási adat</returns>
        public OpeningItem Update(int id, OpeningDTO opening)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(opening), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<OpeningItem>(responseContent);
            }
            else
            {
     
                throw new Exception("A módosítás sikertelen volt.");
            }
        }
    }
}
