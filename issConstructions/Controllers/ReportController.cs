using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issConstructions.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Paymentvoucher(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        // GET: Receipt Voucher
        public ActionResult Receiptvoucher()
        {
            return View();
        }
    }
}