using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace LoginApp.Controllers
{
    public class tblProjectsController : Controller
    {
        private salesDBEntities db = new salesDBEntities();

        // GET: tblProjects
        public ActionResult Index()
        {
            return View(db.tblProjects.ToList());
        }

        // GET: tblProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProject tblProject = db.tblProjects.Find(id);
            if (tblProject == null)
            {
                return HttpNotFound();
            }
            return View(tblProject);
        }

        // GET: tblProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,ProjectLocation,BookingStatus,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,ClubHouseCharges")] tblProject tblProject)
        {
            if (ModelState.IsValid)
            {
                db.tblProjects.Add(tblProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblProject);
        }

        // GET: tblProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProject tblProject = db.tblProjects.Find(id);
            if (tblProject == null)
            {
                return HttpNotFound();
            }
            return View(tblProject);
        }

        // POST: tblProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,ProjectLocation,BookingStatus,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,ClubHouseCharges")] tblProject tblProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblProject);
        }

        // GET: tblProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProject tblProject = db.tblProjects.Find(id);
            if (tblProject == null)
            {
                return HttpNotFound();
            }
            return View(tblProject);
        }

        // POST: tblProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProject tblProject = db.tblProjects.Find(id);
            db.tblProjects.Remove(tblProject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
