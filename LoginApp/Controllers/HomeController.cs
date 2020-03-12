using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            //distance(17.7324687, 17.764513, 83.30409569999999, 83.357335);
            return View();
        }
        

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}