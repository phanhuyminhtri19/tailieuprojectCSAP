using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;

namespace UniHostel.Controllers
{
    public class RentersController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Renters
        public ActionResult Index()
        {
            if (Session["User"] is User user)
            {
                var renters = db.Renters.Where(renter => renter.EndDate == null && renter.Room.HostID == user.ID);
                return View(renters.ToList());
            }
            return RedirectToAction("Index", "Rooms");
        }

        // GET: Renters/Details/5
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

        // GET: Renters/Edit/5
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

        // POST: Renters/Edit/5
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

        // GET: Renters/Delete/5
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

        // POST: Renters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Renter renter = db.Renters.Find(id);
            db.Renters.Remove(renter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MarkLeave(string id)
        {
            var renter = db.Renters.Find(id);
            if(renter != null)
            {
                renter.EndDate = DateTime.Now;
                renter.Room.isAvailable = true;
                db.SaveChanges();
            }
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
