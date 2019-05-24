using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniHostel.ExtensionMethod;
using UniHostel.Models;
using System.Net;
using System.Threading.Tasks;

namespace UniHostel.Controllers
{
    public class HomeController : Controller
    {
        private UniHostelDB _db = new UniHostelDB();

        [HttpGet]
        public ActionResult Login()
        {
            User user = Session["User"] as User;
            if (user != null)
            {
                return Login(user.Username, user.Password, true);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, bool isForward = false)
        {
            try
            {

                string encryptPassword = password;
                if (!isForward)
                {
                    encryptPassword = password.ComputeSha256Hash();
                }
                User user = _db.Users.First<User>(u => u.Username == username
                                                            && u.Password == encryptPassword
                                                            && u.isActive == true);
                if (user != null)
                {
                    Session["User"] = user;
                    Session.Timeout = 30;
                    switch (user.RoleID)
                    {
                        case 1: //Admin
                            return RedirectToAction("Index", "Admin");
                        case 2: // Host
                            return RedirectToAction("Index", "Rooms");
                        case 3:// Renter
                            return RedirectToAction("Index", "Bills");
                    }
                }
            }
            catch (InvalidOperationException)
            {
                ViewBag.Message = "Username or password is not correct";
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetValidUntilExpires(false);
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserRenterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = MyAutoMapper.GetInstance().Map<User>(model);
                var renter = MyAutoMapper.GetInstance().Map<Renter>(model);
                try
                {
                    var check = CheckRoomIDIsAvailable(model.RoomID);
                    if (check == -1)
                    {
                        ModelState.AddModelError("RoomID", "This RoomID is not valid");
                        return View(model);
                    }
                    else if (check == 0)
                    {
                        ModelState.AddModelError("RoomID", "This RoomID is not available");
                        return View(model);
                    }
                    string userID = Utils.getRandomID(10);
                    user.ID = userID;
                    var roleRenterID = _db.Roles.Single(r => r.RoleName == "Renter").RoleID;
                    user.RoleID = roleRenterID;
                    user.isActive = true;
                    user.Password = user.Password.ComputeSha256Hash();
                    _db.Users.Add(user);

                    renter.ID = userID;
                    renter.StartDate = DateTime.Now;
                    renter.RoomID = model.RoomID;
                    _db.Renters.Add(renter);

                    var room = _db.Rooms.Find(model.RoomID);
                    room.isAvailable = false;

                    _db.SaveChanges();

                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View(model);
                }
                return Login(user.Username, user.Password, true);
            }
            return View(model);
        }

        private int CheckRoomIDIsAvailable(string roomID)
        {
            var room = _db.Rooms.Find(roomID);
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

        public JsonResult RecoverPassword(string username)
        {
            var user = _db.Users.Where(u => u.Username == username).FirstOrDefault();
            if(user != null)
            {
                string content = user.ID + user.Username + user.Password;
                string mail = "";
                if (user.Host != null)
                {
                    mail = user.Host.Mail;
                    content += user.Host.FullName + user.Host.Address + user.Host.Phone + user.Host.Mail;
                }
                else
                {
                    mail = user.Renter.Mail;
                    content += user.Renter.FullName + user.Renter.StartDate + user.Renter.Mail + user.Renter.HomeTown + user.Renter.Phone;
                }
                string hash = content.ComputeSha256Hash();
                var host = "http://localhost";//Dns.GetHostName();
                int port = 61368;
                var uri = string.Format("{0}:{1}/Home/ChangePassword?Token={2}&ID={3}", host, port, hash, user.ID);
                string message = GetStringFromView(uri, user);
                Utils.SendingMailUsingMailKit(message, mail);
                string msg = "Email has been sent to your email.<br/> Please check your email carefully to reset password!!!";
                return Json(new { success = true, message = msg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string msg = "Sorry! This username does not exsist";
                return Json(new { success = false, message = msg }, JsonRequestBehavior.AllowGet);
            }
        }

        private string GetStringFromView(string link, object model)
        {
            TempData["Link"] = link;
            return this.ControllerContext.RenderPartial("MailHtmlMessage", model);
        }

        [HttpGet]
        public ActionResult ChangePassword(string Token, string ID)
        {
            try
            {
                var user = _db.Users.Find(ID);
                string content = user.ID + user.Username + user.Password;
                if (user.Host != null)
                {
                    content += user.Host.FullName + user.Host.Address + user.Host.Phone + user.Host.Mail;
                }
                else
                {
                    content += user.Renter.FullName + user.Renter.StartDate + user.Renter.Mail + user.Renter.HomeTown + user.Renter.Phone;
                }
                string hash = content.ComputeSha256Hash();
                if (Token.Equals(hash))
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                Utils.Log(ex.Message);
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordAction(string ID, string password)
        {
            var user = _db.Users.Find(ID);
            try
            {
                user.Password = password.ComputeSha256Hash();
                _db.SaveChanges();
            }catch(Exception ex)
            {
                Utils.Log(ex.Message);
                return View(user);
            }
            return RedirectToAction("Login");
        }

    }
}