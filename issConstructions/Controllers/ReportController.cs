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
   // [CustomAuthorize(Roles = "Admin,Manager")]
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

        public ActionResult Payment(SiteName siteName)
        {
            var lstsitename = db.siteDetails.ToList();
            ViewBag.siteName = lstsitename;
            return View(lstsitename);
        }
        public ActionResult Receipt(SiteName siteName)
        {
            var lstsitename = db.siteDetails.ToList();
            ViewBag.siteName = lstsitename;
            return View(lstsitename);
        }

       public ActionResult PurchaseRequest()
        {
            List<SelectListItem> SiteName = new List<SelectListItem>();
            
            foreach (var item in db.siteDetails.ToList())
            {
                SiteName.Add(new SelectListItem { Text = item.SiteName, Value = item.ID.ToString() });
                
            }
            ViewBag.SiteName = SiteName;

            List<SelectListItem> SupName = new List<SelectListItem>();

            foreach (var item in db.supplierMasters.ToList())
            {
                SupName.Add(new SelectListItem { Text = item.Suppliername, Value = item.ID.ToString() });
            }
            ViewBag.SupName =SupName;
            return View();
        }

        public ActionResult PurchaseOrder()
        {
            List<SelectListItem> SiteName = new List<SelectListItem>();

            foreach (var item in db.siteDetails.ToList())
            {
                SiteName.Add(new SelectListItem { Text = item.SiteName, Value = item.ID.ToString() });

            }
            ViewBag.SiteName = SiteName;

            List<SelectListItem> SupName = new List<SelectListItem>();

            foreach (var item in db.supplierMasters.ToList())
            {
                SupName.Add(new SelectListItem { Text = item.Suppliername, Value = item.ID.ToString() });
            }
            ViewBag.SupName = SupName;
            return View();
        }

        public ActionResult Purchase()
        {
            List<SelectListItem> SiteName = new List<SelectListItem>();

            foreach (var item in db.siteDetails.ToList())
            {
                SiteName.Add(new SelectListItem { Text = item.SiteName, Value = item.ID.ToString() });

            }
            ViewBag.SiteName = SiteName;

            List<SelectListItem> SupName = new List<SelectListItem>();

            foreach (var item in db.supplierMasters.ToList())
            {
                SupName.Add(new SelectListItem { Text = item.Suppliername, Value = item.ID.ToString() });
            }
            ViewBag.SupName = SupName;
            return View();
        }

        public ActionResult PurchaseCat()
        {
            List<SelectListItem> Category = new List<SelectListItem>();

            foreach (var item in db.categoryMasters.ToList())
            {
                Category.Add(new SelectListItem { Text = item.CategoryName, Value = item.ID.ToString() });

            }
            ViewBag.Category = Category;

            List<SelectListItem> ItemName = new List<SelectListItem>();

            foreach (var item in db.productMasters.ToList())
            {
                ItemName.Add(new SelectListItem { Text = item.ProductName, Value = item.ID.ToString() });
            }
            ViewBag.ItemName = ItemName;
            return View();
        }

        public ActionResult PurchaseBill()
        {
            return View();
        }

        public ActionResult Issue(SiteName siteName)
        {
            var lstsitename = db.siteDetails.ToList();
            ViewBag.siteName = lstsitename;
            return View(lstsitename);
        }

        public ActionResult IssueCat()
        {
            List<SelectListItem> Category = new List<SelectListItem>();

            foreach (var item in db.categoryMasters.ToList())
            {
                Category.Add(new SelectListItem { Text = item.CategoryName, Value = item.ID.ToString() });

            }
            ViewBag.Category = Category;

            List<SelectListItem> ItemName = new List<SelectListItem>();

            foreach (var item in db.productMasters.ToList())
            {
                ItemName.Add(new SelectListItem { Text = item.ProductName, Value = item.ID.ToString() });
            }
            ViewBag.ItemName = ItemName;
            return View();
        }

        public ActionResult RateWork(SiteName siteName)
        {
            var lstsitename = db.siteDetails.ToList();
            ViewBag.siteName = lstsitename;
            return View(lstsitename);
        }

        public ActionResult ExtraWork(SiteName siteName)
        {
            var lstsitename = db.siteDetails.ToList();
            ViewBag.siteName = lstsitename;
            return View(lstsitename);
        }

        public ActionResult ToolsTransfer()
        {
            return View();
        }

        #region Methods
        [HttpPost]
        public JsonResult getPurchaseRequestList(string fDate,string tDate)
        {
            DateTime fromDate = Convert.ToDateTime(fDate);
            DateTime toDate = Convert.ToDateTime(tDate);
            var requests = db.purchaseRequest.Where(x => x.RequestDate >= fromDate && x.RequestDate <= toDate).ToList();
            return Json(requests,JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Methods
        [HttpPost]
        public JsonResult getPurchaseOrderList(string fDate, string tDate)
        {
            DateTime fromDate = Convert.ToDateTime(fDate);
            DateTime toDate = Convert.ToDateTime(tDate);
            var orders = db.PurchaseOrders.Where(x => x.OrderDate >= fromDate && x.OrderDate <= toDate).ToList();
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Methods
        [HttpPost]
        public JsonResult getPurchaseList(string fDate, string tDate)
        {
            DateTime fromDate = Convert.ToDateTime(fDate);
            DateTime toDate = Convert.ToDateTime(tDate);
            var orders = db.purchaseEntries.Where(x => x.purchaseDate >= fromDate && x.purchaseDate <= toDate).ToList();
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Methods
        [HttpPost]
        public JsonResult getIssueList(string fDate, string tDate)
        {
            DateTime fromDate = Convert.ToDateTime(fDate);
            DateTime toDate = Convert.ToDateTime(tDate);
            var orders = db.issues.Where(x => x.IssuesDate>= fromDate && x.IssuesDate <= toDate).ToList();
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }

}