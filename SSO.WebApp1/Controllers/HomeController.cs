using SSO.WebApp1.ActionFilters;
using System.Web;
using System.Web.Mvc;

namespace SSO.WebApp1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          //  var tt = HttpContext.Request.Cookies["Access"];
           // Response.Cookies.Add(new HttpCookie("Access", tt.Value));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}