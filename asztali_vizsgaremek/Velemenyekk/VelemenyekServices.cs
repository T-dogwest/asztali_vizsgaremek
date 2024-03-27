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
    internal class VelemenyekServices
    {
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/Review";
        public VelemenyekServices()
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenM.GetToken());
        }
        public List<VelemenyekItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<VelemenyekItem>>(json);
        }
        public bool Delete(VelemenyekItem velemeny)
        {
            int id = velemeny.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
