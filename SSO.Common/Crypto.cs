using SSO.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SSO.Common
{
    public class Crypto : ICrypto
    {
        /// <summary>
        /// Hash password based on given salt, and if salt is null, generate a new salt
        /// </summary>
        /// <param name="plainTextPassword"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public Task<Dictionary<string, byte[]>> EncryptPassword(string plainTextPassword, byte[] salt = null)
        {
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();

            if (salt == null || salt.Length == 0)
                salt = CreateSalt();

            var hashedPassword = GetHashPassword(plainTextPassword, salt, 1000);
            result.Add(Convert.ToBase64String(hashedPassword), salt);
            //result.Add(GetPasswordSaltHashString(hashedPassword, salt), salt);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Create salt based on RNGCryptoServiceProvider
        /// </summary>
        /// <returns></returns>
        private byte[] CreateSalt()
        {
            var result = new byte[16];
            using (var saltGen = new RNGCryptoServiceProvider())
            {
                saltGen.GetBytes(result);
            }

            return result;
        }

        /// <summary>
        /// Hash the password based on Rfc2898DeriveBytes
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iteration"></param>
        /// <returns></returns>
        private byte[] GetHashPassword(string password, byte[] salt, int iteration)
        {
            byte[] result;
            using (var rfc = new Rfc2898DeriveBytes(password, salt, iteration))
            {
                result = rfc.GetBytes(20);
            }

            return result;
        }

        // Not Used
        //private string GetPasswordSaltHashString(byte[] password, byte[] salt)
        //{
        //    string result = null;

        //    byte[] tmp = new byte[password.Length + salt.Length];
        //    Array.Copy(salt, 0, tmp, 0, salt.Length);
        //    Array.Copy(password, 0, tmp, salt.Length, password.Length);

        //    result = Convert.ToBase64String(password);

        //    return result;
        //}
    }
}
