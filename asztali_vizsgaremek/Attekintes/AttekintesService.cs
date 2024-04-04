using asztali_vizsgaremek.Attekintes;
using asztali_vizsgaremek.User;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asztali_vizsgaremek
{
    class AttekintesService
    {
        private string token;
        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/reservation/allRes";
        public AttekintesService()
        {
            token = TokenM.GetToken();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        public List<AttekintesItem> GetAll()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine(json);
                    return JsonConvert.DeserializeObject<List<AttekintesItem>>(json);
                }
                else
                {
                    MessageBox.Show("nono");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }
    }

}


