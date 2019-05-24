using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.ExtensionMethod;
using UniHostel.Models;
using UniHostel.Models.ViewModel;

namespace UniHostel.Controllers
{
    public class UsersController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        public ActionResult Details()
        {
            if (Session["User"] is User userSession)
            {
                string resource = "RenterDetails";
                string id = userSession.ID;
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                if (user.Host != null)
                {
                    resource = "HostDetails";
                }
                return View(resource, user);
            }
            return RedirectToAction("Index", "Rooms");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditHost([Bind(Include = "ID,FullName,Address,Phone,Mail")] Host host)
        {
            db.Hosts.Attach(host);
            var entry = db.Entry(host);
            entry.Property(h => h.FullName).IsModified = true;
            entry.Property(h => h.Address).IsModified = true;
            entry.Property(h => h.Phone).IsModified = true;
            entry.Property(h => h.Mail).IsModified = true;
            db.SaveChanges();
            return RedirectToAction("Details");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRenter([Bind(Include = "ID,FullName,BirthDate,Phone,Mail,HomeTown,RoomID")] Renter renter)
        {
            if (ModelState.IsValid)
            {
                db.Renters.Attach(renter);
                var entry = db.Entry(renter);
                entry.Property(r => r.FullName).IsModified = true;
                entry.Property(r => r.BirthDate).IsModified = true;
                entry.Property(r => r.Phone).IsModified = true;
                entry.Property(r => r.Mail).IsModified = true;
                entry.Property(r => r.HomeTown).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            string messages = string.Join("; ", ModelState.Values
                                         .SelectMany(x => x.Errors)
                                         .Select(x => x.ErrorMessage));
            ModelState.AddModelError(string.Empty, messages);
            return View("RenterDetails");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeRoom(string roomID, string ID)
        {
            var user = Session["User"] as User;
            if(user != null)
            {
                if (CheckRoomIDIsAvailable(roomID) == 1)
                {
                    var renter = db.Renters.Find(user.ID);
                    renter.Room.isAvailable = true;
                    renter.RoomID = roomID;
                    renter.EndDate = null;
                    var room = db.Rooms.Find(roomID);
                    room.isAvailable = false;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "RoomID is not valid");
                    return View("RenterDetails", user);
                }
            }
            return RedirectToAction("Index", "Rooms");
        }

        private int CheckRoomIDIsAvailable(string roomID)
        {
            var room = db.Rooms.Find(roomID);
            if (room != null)
            {
                if (room.isAvailable == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return -1;
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["User"] is User user)
            {
                ChangePasswordViewModel viewModel = new ChangePasswordViewModel(user);
                return View(viewModel);
            }
            return RedirectToAction("Index", "Rooms");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel viewModel)
        {
            User user = db.Users.Find(viewModel.ID);
            if (user != null)
            {
                if (ModelState.IsValid)
                {
                    if (!viewModel.NewPassword.Equals(viewModel.ConfirmPassword))
                    {
                        ModelState.AddModelError("ConfirmPassword", "Password is not matched");
                    }
                    else if (!user.Password.Equals(viewModel.Password.ComputeSha256Hash()))
                    {
                        ModelState.AddModelError("Password", "Password is not true");
                    }
                    else
                    {
                        user.Password = viewModel.NewPassword.ComputeSha256Hash();
                        db.SaveChanges();
                        return RedirectToAction("Details");
                    }
                }
                viewModel.Password = "";
                viewModel.NewPassword = "";
                viewModel.ConfirmPassword = "";
                return View(viewModel);

            }
            return RedirectToAction("Index", "Rooms");
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
