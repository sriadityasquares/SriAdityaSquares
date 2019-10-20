using BusinessLayer;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers.Admin
{
    public class ProjectController : Controller
    {
        public static List<Projects> projectList = new List<Projects>();
        BookingBL booking = new BookingBL();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details()
        {
            projectList = booking.BindProjects();
            foreach (var item in projectList)
            {
                switch (item.BookingStatus)
                {
                    case "O":
                        item.BookingStatusName = "OPEN";
                        break;
                    case "C":
                        item.BookingStatusName = "CLOSED";
                        break;
                }
            }
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        [HttpGet]
        public ActionResult CreateProject(string models)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       
        // POST: Project/Edit/5
        [HttpGet]
        public ActionResult UpdateProject(string models)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<Projects>>(models);
                // TODO: Add update logic here

                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
