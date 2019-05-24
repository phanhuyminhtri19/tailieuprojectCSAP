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
    public class CompulsoryServicesController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: CompulsoryServices
        public ActionResult Index()
        {
            User user = Session["User"] as User;
            if (CheckSession())
            {
                var compulsoryServices = db.CompulsoryServices
                                                .Include(c => c.Host)
                                                .Where(s => s.isActive == true 
                                                            && user.ID == s.HostID);
                return View(compulsoryServices.ToList());
            }
            return RedirectToAction("Index", "Rooms");
        }

        // GET: CompulsoryServices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            if (compulsoryService == null)
            {
                return HttpNotFound();
            }
            return View(compulsoryService);
        }

        // GET: CompulsoryServices/Create
        public ActionResult Create()
        {
            if (CheckSession())
            {
                return View();
            }
            return RedirectToAction("Index", "Rooms");
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Price,Unit,Description,HostID")] CompulsoryService compulsoryService)
        {
            User user = Session["User"] as User;
            if(user != null)
            {
                ModelState["ID"]?.Errors?.Clear();
                if (ModelState.IsValid)
                {
                    string ID = Utils.getRandomID(10);
                    compulsoryService.ID = ID;
                    compulsoryService.isActive = true;
                    db.CompulsoryServices.Add(compulsoryService);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", "Rooms");
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            if (compulsoryService == null)
            {
                return HttpNotFound();
            }
            return View(compulsoryService);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Price,Unit,Description,HostID")] CompulsoryService compulsoryService)
        {
            if (ModelState.IsValid)
            {
                var dbService = db.CompulsoryServices.Find(compulsoryService.ID);
                dbService.Name = compulsoryService.Name;
                dbService.Price = compulsoryService.Price;
                dbService.Unit = compulsoryService.Unit;
                dbService.Description = compulsoryService.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HostID = new SelectList(db.Hosts, "ID", "FullName", compulsoryService.HostID);
            return View(compulsoryService);
        }

        // GET: CompulsoryServices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            if (compulsoryService == null)
            {
                return HttpNotFound();
            }
            return View(compulsoryService);
        }

        // POST: CompulsoryServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CompulsoryService compulsoryService = db.CompulsoryServices.Find(id);
            compulsoryService.isActive = false;
            //db.Entry(compulsoryService).State = EntityState.Modified;
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

        private bool CheckSession()
        {
            User user = Session["User"] as User;
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
