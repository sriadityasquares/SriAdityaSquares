using BusinessLayer;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class HomeController : Controller
    {
        BookingBL booking = new BookingBL();
        AdminBL admin = new AdminBL();
        public ActionResult Index()
        {
            //distance(17.7324687, 17.764513, 83.30409569999999, 83.357335);
            List<Projects> projectList = booking.BindProjects();
            var json = JsonConvert.SerializeObject(projectList);
            ViewBag.GalleryImages = admin.BindProjectGallery("HomePage Gallery");
            string[] headerImg = { "aaView.jpg", "GT.jpg" };
            ViewBag.HeaderImages = headerImg;
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
            TempData.Keep("ProjectList");
            return View();
        }

        public JsonResult GetHeaderImages(string section)
        {
            var result = admin.BindProjectGallery(section);
            foreach(var item in result)
            {
                item.URL = Path.GetFileName(item.URL);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Portfolio()
        {
            //distance(17.7324687, 17.764513, 83.30409569999999, 83.357335);
            List<Projects> projectList = booking.BindProjects();
            var json = JsonConvert.SerializeObject(projectList);
            TempData["ProjectList"] = new SelectList(projectList, "ProjectID", "ProjectName");
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