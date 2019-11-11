using BusinessLayer;
using ModelLayer;
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
        //ProjectBL project = new ProjectBL();
        // GET: Tower
        public ActionResult Index()
        {
            return View();
        }

        // GET: Tower/Details/5
        public ActionResult Details()
        {
            towerList = booking.BindTowerDetails();
            //foreach (var item in towerList)
            //{
            //    switch (item.BookingStatus)
            //    {
            //        case "O":
            //            item.BookingStatusName = "OPEN";
            //            break;
            //        case "C":
            //            item.BookingStatusName = "CLOSED";
            //            break;
            //    }
            //}
            return Json(towerList, JsonRequestBehavior.AllowGet);
        }

        // GET: Tower/Create
        [HttpGet]
        public ActionResult CreateTower(string models)
        {
            return View();
        }


        [HttpGet]
        public ActionResult UpdateTower(string models)
        {
            return View();
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
