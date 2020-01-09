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
    public class TowerController : Controller
    {
        public static List<GetTowerDetails> towerList = new List<GetTowerDetails>();
        BookingBL booking = new BookingBL();
        AdminBL towers = new AdminBL();
        // GET: Tower
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tower/Details/5
        public ActionResult Details()
        {
            towerList = booking.BindTowerDetails();
            return Json(towerList, JsonRequestBehavior.AllowGet);
        }

        // GET: Tower/Create
        [HttpGet]
        public ActionResult CreateTower(string models)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            List<Towers> data = JsonConvert.DeserializeObject<List<Towers>>(models, settings);
            data[0].ProjectID = Convert.ToInt32(data[0].ProjectName);
            var result = towers.AddTower(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UpdateTower(string models)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            List<Towers> data = JsonConvert.DeserializeObject<List<Towers>>(models, settings);
            var result = towers.UpdateTower(data[0]);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        // POST: Tower/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Tower/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tower/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tower/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tower/Delete/5
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
