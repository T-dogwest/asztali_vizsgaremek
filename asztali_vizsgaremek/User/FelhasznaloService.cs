using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    class FelhasznaloService
    {
       
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/user";
        private string url1 = "http://localhost:3000/user/createadmin";
        private string token;


        public FelhasznaloService()
        {
            token = TokenM.GetToken();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }

        public List<FelhasznmalokItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<FelhasznmalokItem>>(json);
        }

        public FelhasznmalokItem Add(FelhasznalokDTO user)
        {
            try
            {
                string body = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = client.PostAsync(url1, content).Result; // Elvégzem a POST kérést

                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Sikeres válasz tartalma:");
                    Console.WriteLine(responseContent); // Kiírjuk a válasz tartalmát
                    return JsonConvert.DeserializeObject<FelhasznmalokItem>(responseContent);
                }
                else if (responseMessage.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Az e-mail cím vagy a felhasználónév már foglalt.");
                }
                else
                {
                    string errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    throw new Exception("Az elem hozzáadása sikertelen volt. Kód: " + responseMessage.StatusCode + ". Hibaüzenet: " + errorMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex; // A dobott kivétel továbbdobjuk
            }
        }

        public bool Delete(FelhasznmalokItem user)
        {
            int id = user.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

       
    }
}
    

