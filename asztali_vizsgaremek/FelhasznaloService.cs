using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    class FelhasznaloService
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/user";

        public FelhasznaloService()
        {

           client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }

        public List<FelhasznmalokItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<FelhasznmalokItem>>(json);
        }

        public FelhasznmalokItem Add(FelhasznalokDTO user
            )
        {
            string body = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;
            string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<FelhasznmalokItem>(responseContent);
        }

        public bool Delete(FelhasznmalokItem user
            )
        {
            int id = user.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public FelhasznmalokItem Update(int id, FelhasznalokDTO user
            )
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<FelhasznmalokItem>(responseContent);
            }
            else
            {
                // Ha valamilyen hiba történt, dobhatunk egy kivételt vagy visszaadhatunk null-t, attól függően, hogy hogyan akarjuk kezelni a hibát
                throw new Exception("A módosítás sikertelen volt.");
            }
        }
    }

}

