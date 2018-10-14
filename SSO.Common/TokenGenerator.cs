using System;
using System.Text;

namespace SSO.Common
{
    public static class TokenGenerator
    {
        /// <summary>
        /// Simple implementation to create access token based on datetime and username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GenerateToken(string user)
        {
            byte[] _time = Encoding.ASCII.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(DateTimeOffset.UtcNow));
            byte[] _Id = Encoding.ASCII.GetBytes(user);
            byte[] data = new byte[_time.Length + _Id.Length];

            Array.Copy(_time, 0, data, 0, _time.Length);
            Array.Copy(_Id, 0, data, _time.Length, _Id.Length);

            return Encoding.ASCII.GetString(data);
        }
    }
}
