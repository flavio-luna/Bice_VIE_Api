using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BanBice.Cl.IndicadoresEconomicos.Api.Helpers
{
    public static class Crypto
    {
        public static string CrearSalt(int size)
        {
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string CrearHashSHA256(string input, string salt) 
        {
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256String = new SHA256Managed();
            var hash = sha256String.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
