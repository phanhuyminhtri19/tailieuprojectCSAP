using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.ExtensionMethod;
using UniHostel.Models;

namespace UniHostel.Controllers
{
    public class AdminController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Host)
                                .Include(u => u.Renter)
                                .Include(u => u.Role)
                                .Where(u => u.RoleID == 2 && u.isActive == true);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, Host host)
        {
            user.ID = Utils.getRandomID(10);
            user.RoleID = int.Parse(ConfigurationManager.AppSettings["HOST"].ToString());
            var userInDb = db.Users.Where(u => u.Username == user.Username);
            if (userInDb.Count() != 0)
            {
                ModelState.AddModelError(String.Empty, "This username already exists.");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                user.isActive = true;
                host.isActive = true;
                user.Host = host;
                string encryptPassword = user.Password.ComputeSha256Hash();
                user.Password = encryptPassword;
                db.Users.Add(user);

                for (int i = 0; i < host.NumOfRoom; i++)
                {
                    Room tmp = null;
                    string id;
                    do
                    {
                        id = Utils.getRandomID(2);
                        tmp = db.Rooms.Find(id);
                    } while (tmp != null);
                    var newRoom = new Room()
                    {
                        Name = (i + 1).ToString(),
                        Price = 1,
                        Square = 1,
                        ID = id,
                        isActive = true,
                        isAvailable = true
                    };
                    host.Rooms.Add(newRoom);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            var usertest = db.Users.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Host host)
        {
            if (ModelState.IsValid)
            {
                db.Entry(host).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(host);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Host host = db.Hosts.Find(id);
            host.isActive = false;
            User user = db.Users.Find(id);
            user.isActive = false;
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
