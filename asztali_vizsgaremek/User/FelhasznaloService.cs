using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace asztali_vizsgaremek.User
{/// <summary>
 /// Felhasználók kezeléséért felelős szolgáltatásosztály
 /// </summary>
    class FelhasznaloService
    {

        private HttpClient client = new HttpClient();
        private string url = "http://localhost:3000/user";
        private string url1 = "http://localhost:3000/user/createadmin";
        private string url2 = "http://localhost:3000/user/me";

        private string token;


        /// <summary>
        /// Felhasználó szolgáltatás konstruktora.
        /// </summary>
        public FelhasznaloService()
        {
            token = TokenM.GetToken();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
        /// <summary>
        /// Az összes felhasználó lekérdezése.
        /// </summary>
        /// <returns>A felhasználók listája.</returns>
        public List<FelhasznmalokItem> GetAll()
        {
            string json = client.GetStringAsync(url).Result;
            Debug.WriteLine(json);
            return JsonConvert.DeserializeObject<List<FelhasznmalokItem>>(json);
        }
        /// <summary>
        /// Új felhasználó hozzáadása.
        /// </summary>
        /// <param name="user">Az új felhasználó adatai.</param>
        /// <returns>A hozzáadott felhasználó adatai.</returns>
        public FelhasznmalokItem Add(FelhasznalokDTO user)
        {
            try
            {
                string body = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = client.PostAsync(url1, content).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Sikeres válasz tartalma:");
                    Console.WriteLine(responseContent);
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
                throw ex;
            }
        }
        /// <summary>
        /// Felhasználó törlése.
        /// </summary>
        /// <param name="user">A törlendő felhasználó adatai.</param>
        /// <returns>True, ha a törlés sikeres, egyébként false.</returns>
        public bool Delete(FelhasznmalokItem user)
        {
            int id = user.Id;
            HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
        /// <summary>
        /// Bejelentkezett felhasználó adatainak lekérdezése.
        /// </summary>
        /// <returns>A bejelentkezett felhasználó adatai.</returns>
        public FelhasznmalokItem GetLoggedInUserData()
        {
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync(url2).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                    var loggedInUser = JsonConvert.DeserializeObject<FelhasznmalokItem>(responseContent);
                    return loggedInUser;
                }
                else
                {
                    Console.WriteLine("A lekérdezés nem sikerült. Státuszkód: " + responseMessage.StatusCode);
                    string errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Hibaüzenet: " + errorMessage);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba történt a lekérdezés során: " + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Felhasználó adatainak frissítése.
        /// </summary>
        /// <param name="id">A frissítendő felhasználó azonosítója.</param>
        /// <param name="updateUserDto">Az új felhasználói adatok.</param>
        public void UpdateUser(int id, UpdateFelhasznaloDTO updateUserDto)
        {
            try
            {
                string updateUrl = $"{url}/{id}";
                string body = JsonConvert.SerializeObject(updateUserDto);
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

                var responseMessage = client.PatchAsync(updateUrl, content).Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception($"Felhasználó frissítése sikertelen. Kód: {responseMessage.StatusCode}");
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Felhasználó jelszavának megváltoztatása.
        /// </summary>
        /// <param name="userId">A felhasználó azonosítója.</param>
        /// <param name="changePasswordDto">Az új jelszó adatok.</param>
        public void ChangePassword(int userId, ChangePasswordDTO changePasswordDto)
        {
            try
            {
                string updateUrl = $"{url}/{userId}/changepass";
                string body = JsonConvert.SerializeObject(changePasswordDto);
                StringContent content = new StringContent(body, Encoding.UTF8, "application/json");

                var responseMessage = client.PatchAsync(updateUrl, content).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    MessageBox.Show("Jelszó sikeresen megváltoztatva!");
                }
                else
                {
                    throw new Exception($"A jelszó módosítása sikertelen. Kód: {responseMessage.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

