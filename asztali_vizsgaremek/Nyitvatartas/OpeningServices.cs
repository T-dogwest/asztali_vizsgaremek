using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek.Nyitvatartas
{
   internal class OpeningServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/opening";
        
        public OpeningServices() 
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }
        public List<OpeningItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<OpeningItem>>(json);
        }
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
