using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Interfaces
{
    public interface ICrypto
    {
        Task<Dictionary<string, byte[]>> EncryptPassword(string plainTextPassword, byte[] salt = null);
    }
}
