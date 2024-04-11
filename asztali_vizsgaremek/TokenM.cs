using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    /// <summary>
    /// A TokenM osztály reprezentál egy token kezelést az autentikációhoz.
    /// </summary>
    public class TokenM
    {
        private static string authToken;
        /// <summary>
        /// Beállítja az authentikációs token értékét.
        /// </summary>
        /// <param name="token">Az authentikációs token.</param>

        public static void SetToken(string token)
        {
            authToken = token;
        }
        /// <summary>
        /// Visszaadja az aktuális authentikációs token értékét.
        /// </summary>
        /// <returns>Az aktuális authentikációs token.</returns>

        public static string GetToken()
        {
            return authToken;
        }
        /// <summary>
        /// Visszaadja az aktuális authentikációs token-t Bearer formátumban.
        /// </summary>
        /// <exception cref="InvalidOperationException">Dobódik, ha a token nincs beállítva.</exception>
        /// <returns>Az aktuális authentikációs token Bearer formátumban.</returns>
        public static string GetBearerToken()
        {
            if (string.IsNullOrEmpty(authToken))
            {
                throw new InvalidOperationException("Token is not set.");
            }

           
            return "Bearer " + authToken;
        }
        /// <summary>
        /// Törli az aktuális authentikációs token-t.
        /// </summary>
        public static void DeleteToken()
        {
            authToken = null; 
        }
    }
}
