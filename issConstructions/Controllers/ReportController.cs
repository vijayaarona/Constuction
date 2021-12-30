using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issConstructions.Custom;
using issConstructions.Models;
using issDomain.Models;

namespace issConstructions.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class ReportController : Controller
    {
        private issDB db = new issDB();

        // GET: Report
        public ActionResult Paymentvoucher(int? id)
        {
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public JsonResult paymentdata(int id)
        {
            var RawData = db.paymentEntries.Where(x => x.ID == id).FirstOrDefault();


            return Json(RawData, JsonRequestBehavior.AllowGet);
        }
        // GET: Receipt Voucher
        public ActionResult Receiptvoucher(int? id)
        {
            ViewBag.ID = id;
            return View();
        }
        [HttpPost]
        public JsonResult receiptdata(int id)
        {
            var RawData = db.receiptEntries.Where(x => x.ID == id).FirstOrDefault();


            return Json(RawData, JsonRequestBehavior.AllowGet);
        }
    }
}