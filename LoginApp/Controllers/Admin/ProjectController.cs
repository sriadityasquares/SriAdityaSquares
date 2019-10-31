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
        ProjectBL project = new ProjectBL();
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
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                List<Projects> data = JsonConvert.DeserializeObject<List<Projects>>(models,settings);
                var result = project.AddProject(data[0]);
                //if (result)
                //{
                //    TempData["successmessage"] = "Added Successfull";
                //}
                //else
                //{
                //    TempData["successmessage"] = "Update Successfull";
                //}
                // TODO: Add update logic here

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

       
        // POST: Project/Edit/5
        [HttpGet]
        public ActionResult UpdateProject(string models)
        {
            try
            {
                List<Projects> data = JsonConvert.DeserializeObject<List<Projects>>(models);
                var result = project.UpdateProject(data[0]);
                //if(result)
                //{
                //    TempData["successmessage"] = "Update Successfull";
                //}
                //else
                //{
                //    TempData["successmessage"] = "Update Successfull";
                //}
                // TODO: Add update logic here

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
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
