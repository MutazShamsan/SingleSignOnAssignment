using SSO.Interfaces;
using System.Threading.Tasks;

namespace SSO.WCF.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SignInService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SignInService.svc or SignInService.svc.cs at the Solution Explorer and start debugging.
    //http://localhost:24672/SignInService.svc/DoWork
    public class SignInService : ISignInService
    {
        private readonly ISignInManagement _signInManagement;
        private readonly ICacheManagement _cacheManagement;

        public SignInService(ISignInManagement signInManagement, ICacheManagement cacheManagement)
        {
            _signInManagement = signInManagement;
        }

        //public async Task<bool> DoWork(string username, string password)
        //{
        //    //await _signInManagement.IsUserAuthorized(username, password);
        //    return await Task.FromResult(true);
        //}

        public async Task<bool> DoWork()
        {
            //await _signInManagement.IsUserAuthorized(username, password);
            return await Task.FromResult(true);
        }
    }
}
