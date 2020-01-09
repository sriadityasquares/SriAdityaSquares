using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers.Admin
{
    public class FlatController : Controller
    {
        // GET: Flat
        public ActionResult Index()
        {
            return View();
        }

        // GET: Flat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Flat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flat/Create
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

        // GET: Flat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Flat/Edit/5
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

        // GET: Flat/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Flat/Delete/5
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
