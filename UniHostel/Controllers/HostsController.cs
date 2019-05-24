using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;

namespace UniHostel.Controllers
{
    public class HostsController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Hosts
        public ActionResult Index()
        {
            //var renters = db.Renters.Include(r => r.Room).Include(r => r.User);
            return View();
        }

        // GET: Hosts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // GET: Hosts/Create
        public ActionResult Create()
        {
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name");
            ViewBag.ID = new SelectList(db.Users, "ID", "Username");
            return View();
        }

        // POST: Hosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FullName,StartDate,EndDate,BirthDate,Mail,HomeTown,Phone,Description,RoomID")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Renters.Add(renter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name", renter.RoomID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Username", renter.ID);
            return View(renter);
        }

        // GET: Hosts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name", renter.RoomID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Username", renter.ID);
            return View(renter);
        }

        // POST: Hosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FullName,StartDate,EndDate,BirthDate,Mail,HomeTown,Phone,Description,RoomID")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(renter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomID = new SelectList(db.Rooms, "ID", "Name", renter.RoomID);
            ViewBag.ID = new SelectList(db.Users, "ID", "Username", renter.ID);
            return View(renter);
        }

        // GET: Hosts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Renter renter = db.Renters.Find(id);
            if (renter == null)
            {
                return HttpNotFound();
            }
            return View(renter);
        }

        // POST: Hosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Renter renter = db.Renters.Find(id);
            db.Renters.Remove(renter);
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
