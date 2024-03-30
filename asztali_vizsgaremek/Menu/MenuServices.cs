using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace asztali_vizsgaremek.Menu.Menu
{

    internal class MenuServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/menu";

        public MenuServices()
        {

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }

        public List<MenuItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<MenuItem>>(json);
        }

        public MenuItem Add(MenuDTO menu)
        {
            string body = JsonConvert.SerializeObject(menu);
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result; // Elvégzem a POST kérést

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<MenuItem>(responseContent);
            }
            else
            {
                // Ha valamilyen hiba történt, dobhatunk egy kivételt vagy visszaadhatunk null-t, attól függően, hogy hogyan akarjuk kezelni a hibát
                throw new Exception("Az elem hozzáadása sikertelen volt.");
            }
        }

        public bool Delete(MenuItem menu)
        {
            int id = menu.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

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
                // Ha valamilyen hiba történt, dobhatunk egy kivételt vagy visszaadhatunk null-t, attól függően, hogy hogyan akarjuk kezelni a hibát
                throw new Exception("A módosítás sikertelen volt.");
            }
        }
    }
}
