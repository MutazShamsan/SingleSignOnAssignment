using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSO.Web.App.Controllers
{
    [Authorize]
    public class NormalController : Controller
    {
        // GET: Normal
        public ActionResult Index()
        {
            return View();
        }
    }
}