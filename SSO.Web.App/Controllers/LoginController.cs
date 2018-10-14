using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SSO.Interfaces;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SSO.Web.App.Controllers
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

        /// <summary>
        /// Start the login process
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(SSO.DataModel.LoginRequsetModel request)
        {
            DataModel.LoginResposeModel serviceResponse = new DataModel.LoginResposeModel();

            request.RequestDateTime = DateTimeOffset.UtcNow;
            request.RequestIpAddress = Request.UserHostAddress;

            try
            {
                //call the service to do the login
                serviceResponse = AuthService.Login(request);

                //Validate the service call result
                if (serviceResponse.IsAuthorized)
                {
                    AuthenticationProperties options = new AuthenticationProperties();

                    options.AllowRefresh = true;
                    options.IsPersistent = true;
                    options.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);


                    //Add the authorized user to the OWIN middleware
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

            //Based on the user level, redirect to the proper page
            if (serviceResponse.UserLevel == 1)
                return Json(Url.Action("Index", "Admin"));
            else if (serviceResponse.UserLevel == 2)
                return Json(Url.Action("Index", "Normal"));
            else
                return Json("User Not Found|");
        }

        /// <summary>
        /// Start the registration process
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(SSO.DataModel.RegistrationRequestModel request)
        {
            DataModel.RegistrationResposeModel serviceResponse = new DataModel.RegistrationResposeModel();

            request.RequestDateTime = DateTimeOffset.UtcNow;
            request.RequestIpAddress = Request.UserHostAddress;

            try
            {
                //Call the external service
                serviceResponse = AuthService.Register(request);
            }
            catch (Exception e)
            {
                serviceResponse.ErrorMessage = "Failed to register user in service";
                AppLogger.Error(e, serviceResponse.ErrorMessage);
            }

            return Json(serviceResponse.ErrorMessage ?? "User Registered");
        }

        /// <summary>
        /// Logout and clear the OWIN Cookie
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logout(SSO.DataModel.LoginRequsetModel request)
        {
            request.RequestDateTime = DateTimeOffset.UtcNow;
            request.RequestIpAddress = Request.UserHostAddress;

            try
            {
                //Call the external service to remove the login cache
                var serviceResponse = AuthService.Logout(request);

                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch (Exception e)
            {
                AppLogger.Error(e, "Failed to logout from service");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}