using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Criptografia
{
     public class HashPassword
    {
        private static HashAlgorithm Algorithm;

        public static string EncryptPassword(string password)
        {
            Algorithm = SHA256.Create();
            var encodedPass = Encoding.UTF8.GetBytes(password);
            var encrypPass = Algorithm.ComputeHash(encodedPass);
            var sb = new StringBuilder();
            foreach (var caracter in encrypPass)
            {
                sb.Append(caracter.ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool VerifyPass(string userPass, string CryptPass)
        {
            string senha = EncryptPassword(userPass);
            return senha == CryptPass;
        }
    }
}

