using Microsoft.Owin.Security;
using SSO.DataModel;
using SSO.Interfaces;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SSO.WebApp1.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IAuthProvider auth, ILogger logger) : base(auth, logger)
        { }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(SSO.DataModel.SignInRequsetModel request)
        {
            DataModel.SignInResposeModel serviceResponse = new SignInResposeModel();

            request.RequestDateTime = DateTimeOffset.UtcNow;
            request.RequestIpAddress = Request.UserHostAddress;

            try
            {
                serviceResponse = AuthService.SignIn(request);

                if (serviceResponse.IsAuthorized)
                {
                    System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie("Token") { Name = "Token", Value = serviceResponse.AccessToken });
                    //Response.Cookies.Add(new HttpCookie("Token") { Name = "Token", Value = serviceResponse.AccessToken });
                    //ViewData["IsFromCache"] = serviceResponse.IsFromCache;

                    AuthenticationProperties options = new AuthenticationProperties();

                    options.AllowRefresh = true;
                    options.IsPersistent = true;
                    options.ExpiresUtc = DateTime.UtcNow.AddMinutes(30);


                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, request.Username),
                        new Claim(ClaimTypes.Role, serviceResponse.UserLevel.ToString()),
                        new Claim("AcessToken", string.Format("Bearer {0}", serviceResponse.AccessToken)),
                    };

                    var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(options, identity);
                }
            }
            catch (Exception e)
            {
                serviceResponse.ErrorMessage = "Failed to login user in service";
                AppLogger.Error(e, serviceResponse.ErrorMessage);
            }

            if(serviceResponse.UserLevel == 1)
            return RedirectToAction("Index", "Admin");
            else
                return RedirectToAction("Index", "Normal");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(SSO.DataModel.RegistrationRequestModel request)
        {
            DataModel.RegistrationResposeModel serviceResponse = new RegistrationResposeModel();

            request.RequestDateTime = DateTimeOffset.UtcNow;
            request.RequestIpAddress = Request.UserHostAddress;

            try
            {
                serviceResponse = AuthService.Register(request);
            }
            catch (Exception e)
            {
                serviceResponse.ErrorMessage = "Failed to register user in service";
                AppLogger.Error(e, serviceResponse.ErrorMessage);
            }

            return Json(serviceResponse.ErrorMessage ?? "User Registered");
        }
    }
}