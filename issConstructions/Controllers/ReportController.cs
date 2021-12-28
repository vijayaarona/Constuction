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
        public ActionResult Paymentvoucher(int? ID)
        {
            ViewBag.ID = ID;
            return View();
        }
        // GET: Receipt Voucher
        public ActionResult Receiptvoucher(int? ID)
        {
            ViewBag.ID = ID;
            return View();
        }
        [HttpPost]
        public JsonResult receiptdata(int ID)
        {
            var RawData = db.receiptEntries.Where(x => x.ID == ID).FirstOrDefault();


            return Json(RawData, JsonRequestBehavior.AllowGet);
        }
    }
}