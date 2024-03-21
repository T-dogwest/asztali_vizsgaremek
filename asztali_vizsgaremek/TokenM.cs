using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asztali_vizsgaremek
{
    public class TokenM
    {
        private static string authToken;

        public static void SetToken(string token)
        {
            authToken = token;
        }

        public static string GetToken()
        {
            return authToken;
        }
        public static string GetBearerToken()
        {
            if (string.IsNullOrEmpty(authToken))
            {
                throw new InvalidOperationException("Token is not set.");
            }

           
            return "Bearer " + authToken;
        }
        public static void DeleteToken()
        {
            authToken = null; 
        }
    }
}
