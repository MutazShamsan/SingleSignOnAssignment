using SSO.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Interfaces
{
    public interface IAppUserRepository : IRepositoryBase
    {
        bool RegisterNewUser(AppUser user);
        AppUser GetUser(string username, string hashedPassword);
        AppUser GetUser(int id);
        string GetPasswordSalt(string username);
    }
}
