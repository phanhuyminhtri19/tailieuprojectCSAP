using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniHostel.Models;
using UniHostel.ExtensionMethod;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace UniHostel.Controllers
{
    public class BillsController : Controller
    {
        private UniHostelDB db = new UniHostelDB();

        public ActionResult Index()
        {
            if (Session["User"] is User user)
            {
                IEnumerable<Bill> bills = null;
                if (user.Host == null)
                {
                    bills = db.Bills.Where(bill => bill.RenterID == user.ID);
                }
                else
                {
                    bills = db.Bills.Where(bill => bill.Room.HostID == user.ID);
                }
                bills = bills.OrderByDescending(b => b.Time);
                return View(bills.ToList());
            }
            return RedirectToAction("Index", "Rooms");

        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        public ActionResult Create()
        {
            if (Session["User"] is User user)
            {
                PrepairForCreatingBill(user);
                var Bill = new Bill();
                return View(Bill);
            }
            return RedirectToAction("Index", "Rooms");
        }

        private void PrepairForCreatingBill(User user)
        {
            ViewBag.RenterID = new SelectList(db.Renters, "ID", "FullName");
            var rooms = db.Rooms.Where(r => r.HostID == user.ID && r.Host.isActive == true && r.isAvailable == false
                                            && !r.Bills.Any(b => DateTime.Now.Month == b.Time.Month && DateTime.Now.Year == b.Time.Year));
            List<CompulsoryService> compulsoryServices = db.CompulsoryServices.Where(c => c.isActive == true && c.HostID == user.ID)
                                                                              .ToList();
            List<AdvancedService> advancedServices = db.AdvancedServices.Where(a => a.isActive == true && a.HostID == user.ID)
                                                                              .ToList();
            ViewData["CompulsoryService"] = compulsoryServices;
            ViewData["AdvancedService"] = advancedServices;
            TempData["RoomID"] = rooms.Select(r => new SelectListItem()
            {
                Text = r.Name + " - " + r.Renters.Where(renter => renter.RoomID == r.ID
                                                       && renter.EndDate == null)
                                               .Select(renter => renter.FullName)
                                               .FirstOrDefault(),
                Value = r.ID
            });
        }

        private bool CompareIgnoreDay(DateTime d1, DateTime d2)
        {
            if (d1.Month == d2.Month && d2.Year == d1.Year)
            {
                return true;
            }
            return false;
        }

        public JsonResult GetPreviousNumber(string CompServiceId, string roomID)
        {
            DateTime now = DateTime.Now;
            var ChosenBill = db.Bills.Where(bill => bill.RoomID == roomID)
                                     .OrderByDescending(bill => bill.Time)
                                     .DefaultIfEmpty(null)
                                     .First();
            if (ChosenBill != null)
            {
                var detailsBill = ChosenBill.BillCompulsoryServiceDetails.Where(details => details.CompulsoryServiceID == CompServiceId)
                                                       .First();
                return Json(detailsBill.CurNum, JsonRequestBehavior.AllowGet);

            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ID,Time")] Bill bill,
            string ICollectionCompServicesJSON,
            string ICollectionAdvancedServicesJSON)
        {
            if (Session["User"] is User user)
            {
                var compulsoryServiceDetailList
    = JsonConvert.DeserializeObject<ICollection<BillCompulsoryServiceDetail>>(ICollectionCompServicesJSON);

                var advancedServiceDetailList
    = JsonConvert.DeserializeObject<ICollection<BillAdvancedServiceDetail>>(ICollectionAdvancedServicesJSON);
                PrepairForCreatingBill(user);
                var roomID = ModelState["RoomID"].Value.AttemptedValue as string;
                var renterID = db.Rooms.Find(roomID).Renters.OrderByDescending(renter => renter.StartDate).FirstOrDefault().ID;
                if (ModelState["RenterID"] != null)
                    ModelState["RenterID"].Errors.Clear();
                bill.RenterID = renterID;
                if (ModelState.IsValid)
                {
                    string billID = Utils.getRandomID(10);
                    bill.ID = billID;
                    bill.isPaid = false;
                    bill.Time = DateTime.Now;
                    foreach (var details in compulsoryServiceDetailList)
                    {
                        if (details.PreNum >= details.CurNum)
                        {
                            ModelState.AddModelError(string.Empty, "Previous number cannot less than current number");
                            return View(bill);
                        }
                        //details.BillID = billID;
                    }

                    //foreach (var details in advancedServiceDetailList)
                    //{
                    //    details.BillID = billID;
                    //}
                    bill.BillCompulsoryServiceDetails = compulsoryServiceDetailList;
                    bill.BillAdvancedServiceDetails = advancedServiceDetailList;
                    db.Bills.Add(bill);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                string messages = string.Join(" ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));
                ModelState.AddModelError(string.Empty, messages);

                return View(bill);
            }
            return RedirectToAction("Index", "Rooms");
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Time,OtherFee,Total,isPaid,Description,RoomID")] Bill newBill,
            string ICollectionCompServicesJSON,
            string ICollectionAdvancedServicesJSON)
        {
            var compulsoryServiceDetailList
= JsonConvert.DeserializeObject<ICollection<BillCompulsoryServiceDetail>>(ICollectionCompServicesJSON);

            var advancedServiceDetailList
= JsonConvert.DeserializeObject<ICollection<BillAdvancedServiceDetail>>(ICollectionAdvancedServicesJSON);
            var oldBill = db.Bills.Find(newBill.ID);

            if (ModelState.IsValid)
            {
                newBill.Time = DateTime.Now;
                for (int i = 0; i < compulsoryServiceDetailList.Count(); i++)
                {
                    var details = compulsoryServiceDetailList.ElementAt(i);
                    int detailsID = oldBill.BillCompulsoryServiceDetails.ElementAt(i).ID;
                    details.ID = detailsID;
                    details.BillID = newBill.ID;
                    db.BillCompulsoryServiceDetails.AddOrUpdate(detail => detail.ID, details);
                }
                for (int i = 0; i < advancedServiceDetailList.Count(); i++)
                {
                    var details = advancedServiceDetailList.ElementAt(i);
                    int detailsID = oldBill.BillAdvancedServiceDetails.ElementAt(i).ID;
                    details.ID = detailsID;
                    details.BillID = newBill.ID;
                    db.BillAdvancedServiceDetails.AddOrUpdate(detail => detail.ID, details);
                }
                newBill.BillCompulsoryServiceDetails = compulsoryServiceDetailList;
                newBill.BillAdvancedServiceDetails = advancedServiceDetailList;
                db.Bills.AddOrUpdate(bill => bill.ID, newBill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newBill);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateIsPaid(string ID, bool value)
        {
            var bill = db.Bills.Find(ID);
            bill.isPaid = value;
            db.Entry(bill).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", new { id = bill.ID });
        }

        public JsonResult GetRoomPrice(string RoomID)
        {
            var room = db.Rooms.Find(RoomID);
            if (room != null)
            {
                return Json(new { success = true, data = room.Price }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Cannot find room with ID " + RoomID }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                Bill bill = db.Bills.Find(id);
                db.BillCompulsoryServiceDetails.Remove<BillCompulsoryServiceDetail>(detail => detail.BillID == bill.ID);
                db.BillAdvancedServiceDetails.Remove<BillAdvancedServiceDetail>(detail => detail.BillID == bill.ID);
                db.Bills.Remove(bill);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportExcel(string id)
        {
            User user = Session["User"] as User;
            string filename = null;
            if (user != null)
            {
                filename = GetExcelFile(id, user);
                ProcessFile(filename);
            }
            return RedirectToAction("Index", "Rooms");
        }

        private void ProcessFile(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (file.Exists)
            {
                HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition",
                                   "attachment; filename=" + file.Name + ";");
                response.TransmitFile(filename);
                response.Flush();
                response.End();
            }

        }

        private string GetExcelFile(string billID, User user)
        {
            string filename = null;
            Application excel = null;
            Workbook wb = null;
            try
            {
                excel = new Application();
                wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                wb.Worksheets.Add();
                Worksheet ws = wb.Worksheets[1];
                ws.Cells.Font.Size = 14;
                ws.Cells.Font.Name = "Times New Roman";
                Bill bill = db.Bills.Find(billID);
                var room = db.Bills.Where(b => b.ID == billID).FirstOrDefault().Room;
                Renter renter = db.Renters.FirstOrDefault(r => r.RoomID == bill.RoomID && r.EndDate == null);
                Host host = db.Hosts.FirstOrDefault(h => h.ID == user.ID);
                var CompDetailsList = db.BillCompulsoryServiceDetails.Where(details => details.BillID == billID);
                var AdvancedDetailsList = db.BillAdvancedServiceDetails.Where(details => details.BillID == billID);
                if (host != null)
                {
                    ws.Cells[1, 1] = "Nhà trọ: " + host.FullName;
                    ws.Cells[2, 1] = "Địa chỉ: " + host.Address;
                    ws.Cells[3, 1] = "Số điện thoại: " + host.Phone;

                    ws.Cells[1, 1].Font.Italic = true;
                    ws.Cells[2, 1].Font.Italic = true;
                    ws.Cells[3, 1].Font.Italic = true;

                    ws.Cells[1, 1].Font.Bold = true;
                    ws.Cells[2, 1].Font.Bold = true;
                    ws.Cells[3, 1].Font.Bold = true;

                    ws.Cells[3, 4] = "Ngày in: " + DateTime.Now.ToShortDateString();
                    ws.Cells[3, 4].Font.Italic = true;

                    Range BillNameRange = ws.Range[ws.Cells[5, 1], ws.Cells[5, 6]];
                    BillNameRange.Merge();
                    BillNameRange.Font.Bold = true;
                    BillNameRange.Font.Size = 18;
                    //BillNameRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.Cells[5, 1] = "HÓA ĐƠN THÀNH TOÁN TIỀN PHÒNG";


                    Range DateRange = ws.Range[ws.Cells[6, 1], ws.Cells[6, 6]];
                    //DateRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    ws.Cells[6, 1] = "From " + bill.Time.ToShortDateString();

                    ws.Cells[8, 1] = "Renter fullname";
                    ws.Cells[9, 1] = "Room";
                    ws.Cells[10, 1] = "Time";

                    ws.Cells[8, 1].Font.Bold = true;
                    ws.Cells[9, 1].Font.Bold = true;
                    ws.Cells[10, 1].Font.Bold = true;

                    ws.Cells[8, 2] = renter.FullName;
                    ws.Cells[9, 2] = renter.Room.Name;
                    ws.Cells[10, 2] = bill.Time.ToShortDateString();

                    ws.Range[ws.Cells[8, 4], ws.Cells[8, 6]].Merge();
                    ws.Cells[8, 4] = "Total (VNĐ)";
                    ws.Range[ws.Cells[9, 4], ws.Cells[10, 6]].Merge();
                    ws.Cells[9, 4] = bill.Total.ToString("N0");

                    ws.Range[ws.Cells[12, 1], ws.Cells[12, 2]].Merge();
                    ws.Cells[12, 1] = "Compulsory Service";
                    ws.Cells[12, 1].Font.Bold = true;
                    ws.Cells[12, 1].Font.Size = 18;

                    ws.Cells[13, 1] = "Service Name";
                    ws.Cells[13, 2] = "Previous Number";
                    ws.Cells[13, 3] = "Current Number";
                    ws.Cells[13, 4] = "Unit";
                    ws.Cells[13, 5] = "Price";
                    ws.Cells[13, 6] = "Amount";

                    ws.Cells[13, 1].Font.Bold = true;
                    ws.Cells[13, 2].Font.Bold = true;
                    ws.Cells[13, 3].Font.Bold = true;
                    ws.Cells[13, 4].Font.Bold = true;
                    ws.Cells[13, 5].Font.Bold = true;
                    ws.Cells[13, 6].Font.Bold = true;

                    int i = 14;
                    foreach (var details in CompDetailsList)
                    {
                        var service = db.CompulsoryServices.Find(details.CompulsoryServiceID);
                        ws.Cells[i, 1] = service.Name;
                        ws.Cells[i, 2] = details.PreNum;
                        ws.Cells[i, 3] = details.CurNum;
                        ws.Cells[i, 4] = service.Unit;
                        ws.Cells[i, 5] = service.Price.ToString("N0");
                        ws.Cells[i, 6] = details.Amount.ToString("N0");
                        i++;
                    }
                    Range CompRange = ws.Range[ws.Cells[13, 1], ws.Cells[i - 1, 6]];
                    CompRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    CompRange.Borders.Weight = XlBorderWeight.xlMedium;

                    i++;
                    ws.Range[ws.Cells[i, 1], ws.Cells[i, 2]].Merge();
                    ws.Cells[i, 1] = "Advanced Service";
                    ws.Cells[i, 1].Font.Bold = true;
                    ws.Cells[i, 1].Font.Size = 18;

                    i++;
                    ws.Cells[i, 1] = "Service Name";
                    ws.Cells[i, 2] = "Unit";
                    ws.Cells[i, 3] = "Quantity";
                    ws.Cells[i, 4] = "Price";
                    ws.Cells[i, 5] = "Amount";

                    ws.Cells[i, 1].Font.Bold = true;
                    ws.Cells[i, 2].Font.Bold = true;
                    ws.Cells[i, 3].Font.Bold = true;
                    ws.Cells[i, 4].Font.Bold = true;
                    ws.Cells[i, 5].Font.Bold = true;

                    int j = i;
                    i++;
                    foreach (var details in AdvancedDetailsList)
                    {
                        var service = db.AdvancedServices.Find(details.AdvancedServiceID);
                        ws.Cells[i, 1] = service.Name;
                        ws.Cells[i, 2] = service.Unit;
                        ws.Cells[i, 3] = details.Quantity;
                        ws.Cells[i, 4] = service.Price.ToString("N0");
                        ws.Cells[i, 5] = details.Amount.ToString("N0");
                        i++;
                    }
                    Range AdvancedRange = ws.Range[ws.Cells[j, 1], ws.Cells[i - 1, 5]];
                    AdvancedRange.Borders.LineStyle = XlLineStyle.xlContinuous;
                    AdvancedRange.Borders.Weight = XlBorderWeight.xlMedium;

                    i++;
                    ws.Range[ws.Cells[i, 1], ws.Cells[i, 2]].Merge();
                    ws.Cells[i, 1] = "Please make a bill payment on time";

                    i++;
                    ws.Cells[i, 1] = "Room Price: ";
                    ws.Cells[i, 1].Font.Bold = true;
                    ws.Cells[i, 2] = bill.Room.Price.ToString("N0");
                    i++;
                    ws.Cells[i, 1] = "Other Fee: ";
                    ws.Cells[i, 1].Font.Bold = true;
                    ws.Cells[i, 2] = bill.OtherFee;
                    i++;
                    ws.Cells[i, 1] = "Description: ";
                    ws.Cells[i, 1].Font.Bold = true;
                    ws.Cells[i, 2] = bill.Description;

                    i++;
                    ws.Range[ws.Cells[i, 4], ws.Cells[i, 6]].Merge();
                    ws.Cells[i, 4] = "Chủ nhà";
                    ws.Cells[i, 4].Font.Bold = true;

                    i += 3;
                    ws.Range[ws.Cells[i, 4], ws.Cells[i, 6]].Merge();
                    ws.Cells[i, 4] = host.FullName;

                    Range CellRange = ws.Range[ws.Cells[1, 1], ws.Cells[i, 6]];
                    CellRange.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlMedium);

                    ws.Cells.Style.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    ws.Columns.AutoFit();
                    ws.Rows.AutoFit();

                    filename = AppDomain.CurrentDomain.BaseDirectory +
                                        renter.FullName + " " + DateTime.Now.ToString("MM-yyyy") + ".xlsx";
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                    wb.SaveAs(filename);
                }
            }
            finally
            {
                if (wb != null)
                {
                    wb.Close(false);
                }

                if (excel != null)
                {
                    excel.Quit();
                }
            }
            return filename;
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
