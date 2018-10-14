using SSO.DataModel;
using SSO.Interfaces;
using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SSO.WebApp1.ActionFilters
{
    public class MyActionFilter : AuthorizeAttribute
    {
        //public override async void OnActionExecuting(ActionExecutingContext filterContext)
        // {
        //     // TODO: Add your action filter's tasks here
        //     SignInService.Service1Client ss = new Service1Client();
        //     //var tt = await ss.RegisterNewUserContractAsync(new AppUser() { Password = "TEST", FullName = "Mutaz", Username = "Mutaz1", UserLevel = 1 });

        //     //if (tt.Success)
        //     //{
        //     //    string sss;
        //     //    sss = "";
        //     //}

        //     //var ttt = await ss.RegisterNewUserContractAsync(new AppUser() { Password = "TEST", FullName = "Mutaz", Username = "Mutaz1", UserLevel = 1 });
        //    // filterContext.HttpContext.
        //     var ttt = await ss.SigninContractAsync(new  SignInRequsetModel() { Password = "TEST", Username = "Mutaz1" });

        //     if (ttt.Success)
        //     {
        //         filterContext.Controller.ViewBag["Token"] = ttt;
        //         string sss;
        //         sss = "";
        //     }
        // }

        //public override async void OnActionExecuted(ActionExecutedContext filterContext)
        //{
        //    var ss = filterContext.Controller.ViewBag["Token"] as SignInResposeModel;

        //    if (ss.Success)
        //    {
        //        string sss = "";
        //        sss = "";
        //    }

        //    //sw.Stop();
        //    //string message = $"<div>Elapsed: {sw.ElapsedMilliseconds} ms</div>";
        //    //byte[] bytes = Encoding.Unicode.GetBytes(message);
        //    //context.HttpContext.Response.Headers.Add("Perf", new string[] { message });
        //    //context.HttpContext.Response.Body.Write(bytes, 0, bytes.Length);
        //}

        public IAuthProvider Provider { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string auth = filterContext.HttpContext.Request.Headers["Authorization"];

            // SignInService.Service1Client ss = new Web.Logic.WcfAuthService.Service1Client();
            var ttt = Provider.SignIn(new SignInRequsetModel() { Password = "TEST", Username = "Mutaz1" });

            if (!ttt.Success || !ttt.IsAuthorized)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                HttpContext.Current.Request.Cookies.Add(new HttpCookie("Access", Newtonsoft.Json.JsonConvert.SerializeObject(ttt)));
                //filterContext.HttpContext.Request.Cookies.Add(new HttpCookie("Access", Newtonsoft.Json.JsonConvert.SerializeObject(ttt)));

                var currentPrincipal = new GenericPrincipal(new GenericIdentity("Mutaz"), null);
                filterContext.HttpContext.User = currentPrincipal;
                Thread.CurrentPrincipal = currentPrincipal;
                HttpContext.Current.User = currentPrincipal;

                //var cache = filterContext.HttpContext.Response.Cache;
                //cache.SetProxyMaxAge(new TimeSpan(0, 0, 40, 0));
                //cache.AddValidationCallback(CacheValidateHandler, ttt);

                //filterContext.Controller.ViewData["Token"] = ttt;
                base.OnAuthorization(filterContext);
            }
        }

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }
    }
}