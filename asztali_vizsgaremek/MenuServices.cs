using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace asztali_vizsgaremek
{
    internal class MenuServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/menu";

        public List<Menucs> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<Menucs>>(json);
        }

        public Menucs Add(Menucs menu)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(menu), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Menucs>(responseContent);
        }

        public bool Delete(Menucs menu)
        {
            int id = menu.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public Menucs Update(int id, Menucs person)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Menucs>(responseContent);
        }


    }
}
