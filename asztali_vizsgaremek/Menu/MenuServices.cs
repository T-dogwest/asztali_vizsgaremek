using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace asztali_vizsgaremek.Menu
{
    /// <summary>
    ///A menu kezeléséért felelős szolgáltatásosztály
    /// </summary>
    internal class MenuServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/menu";

        /// <summary>
        /// Az osztály konstruktora, beállítja az alapértelmezett kérések fejlécét a token alapján.
        /// </summary>
        public MenuServices()
        {

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }
        /// <summary>
        /// Az összes étlap elem lekérdezése.
        /// </summary>
        /// <returns>Az összes étlap elem listája.</returns>
        public List<MenuItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<MenuItem>>(json);
        }
        /// <summary>
        /// Új étlap elem hozzáadása.
        /// </summary>
        /// <param name="menu">Az új étlap elem adatai.</param>
        /// <returns>Az újonnan hozzáadott étlap elem.</returns>
        public MenuItem Add(MenuDTO menu)
        {
            string body = JsonConvert.SerializeObject(menu);
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result; 

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<MenuItem>(responseContent);
            }
            else
            {
                throw new Exception("Az elem hozzáadása sikertelen volt.");
            }
        }
        /// <summary>
        /// Étlap elem törlése.
        /// </summary>
        /// <param name="menu">Az étlap elem, amelyet törölni kell.</param>
        /// <returns>True, ha a törlés sikeres volt, különben false.</returns>
        public bool Delete(MenuItem menu)
        {
            int id = menu.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// Étlap elem frissítése.
        /// </summary>
        /// <param name="id">Az étlap elem azonosítója, amelyet frissíteni kell.</param>
        /// <param name="menu">Az új étlap elem adatai.</param>
        /// <returns>A frissített étlap elem.</returns>
        public MenuItem Update(int id, MenuDTO menu)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<MenuItem>(responseContent);
            }
            else
            {
                throw new Exception("A módosítás sikertelen volt.");
            }
        }
    }
}
