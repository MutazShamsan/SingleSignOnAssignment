//using SSO.WebApp1.SignInService;
using SSO.Interfaces;
using System.Web.Mvc;

namespace SSO.WebApp1.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IAuthProvider AuthService { get; }
        protected ILogger AppLogger { get; }

        protected BaseController(IAuthProvider authService, ILogger appLogger)
        {
            AuthService = authService;
            AppLogger = appLogger;

        }

    }
}