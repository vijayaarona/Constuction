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
    public class IssuesController : Controller
    {
        private issDB db = new issDB();

        // GET: Issues
        public ActionResult Index()
        {
            
            var issues = db.issues.Include(p => p.GName).Include(p => p.SiteDetails);
            return View(issues.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));

        }

        // GET: Issues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issues issues = db.issues .Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (issues == null)
            {
                return HttpNotFound();
            }
            //Category
            List<SelectListItem> Category = new List<SelectListItem>();
            Category.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.categoryMasters.ToList())
            {
                Category.Add(new SelectListItem { Text = item.CategoryName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.CategoryId = Category;
            //From Location
            List<SelectListItem> GName = new List<SelectListItem>();
            GName.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.godowns.ToList())
            {
                GName.Add(new SelectListItem { Text = item.godownName.ToString(), Value = item.Id.ToString() });
            }
            ViewBag.GNameId = GName;
            //To Location
            List<SelectListItem> SiteName = new List<SelectListItem>();
            SiteName.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteName.Add(new SelectListItem { Text = item.SiteName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteNameId = SiteName;
            //Project
            List<SelectListItem> Project = new List<SelectListItem>();
            Project.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Project.Add(new SelectListItem { Text = item.ProjectName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.ProjectId = Project;
            //SiteName
            List<SelectListItem> Site = new List<SelectListItem>();
            Site.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Site.Add(new SelectListItem { Text = item.SiteName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteId = Site;
            //Site Address
            List<SelectListItem> SiteAddress = new List<SelectListItem>();
            SiteAddress.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteAddress.Add(new SelectListItem { Text = item.SiteAddress.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteAddressId = SiteAddress;
            //Product
            var listItems = new SelectList(db.productMasters, "ID", "ProductName");
            List<SelectListItem> Product = new List<SelectListItem>();
            foreach (var item in db.productMasters.ToList())
            {
                Product.Add(new SelectListItem { Text = item.ProductName, Value = item.ID.ToString() });
            }
            ViewBag.ProductId = Product;
            //Tax
            var listsItem = new SelectList(db.productMasters, "ID", "Tax");
            List<SelectListItem> Tax = new List<SelectListItem>();
            foreach (var items in db.productMasters.ToList())
            {
                Tax.Add(new SelectListItem { Text = items.Tax.ToString(), Value = items.ID.ToString() });
            }
            ViewBag.ProductTax = Tax;

            //product No
            int proNo = 0;
            var productNo = db.issues.Where(p => p.IssueID != null).ToList();

            if (productNo.Count > 0)
            {
                proNo = productNo.Max(x => x.IssueID);
            }
            else proNo = 1;
            ViewBag.ProductNo = proNo;
            return View(issues);
        }

        // GET: Issues/Create
        public ActionResult Create()
        {
            List<SelectListItem> Category = new List<SelectListItem>();
            Category.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.categoryMasters.ToList())
            {
                Category.Add(new SelectListItem { Text = item.CategoryName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.CategoryId = Category;

            List<SelectListItem> Project = new List<SelectListItem>();
            Project.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Project.Add(new SelectListItem { Text = item.ProjectName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.ProjectId = Project;
            List<SelectListItem> Site = new List<SelectListItem>();
            Site.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                Site.Add(new SelectListItem { Text = item.SiteName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteId = Site;
            List<SelectListItem> SiteAddress = new List<SelectListItem>();
            SiteAddress.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteAddress.Add(new SelectListItem { Text = item.SiteAddress.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteAddressId = SiteAddress;
            var listItems = new SelectList(db.productMasters, "ID", "ProductName");
            List<SelectListItem> Product = new List<SelectListItem>();
            foreach (var item in db.productMasters.ToList())
            {
                Product.Add(new SelectListItem { Text = item.ProductName, Value = item.ID.ToString() });
            }
            ViewBag.ProductId = Product;

            List<SelectListItem> GName= new List<SelectListItem>();
            GName.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.godowns.ToList())
            {
                GName.Add(new SelectListItem { Text = item.godownName.ToString(), Value = item.Id.ToString() });
            }
            ViewBag.GNameId = GName;
           
            List<SelectListItem> SiteDetails = new List<SelectListItem>();
            SiteDetails.Add(new SelectListItem { Text = "---Please Select---", Value = "0" });
            foreach (var item in db.siteDetails.ToList())
            {
                SiteDetails.Add(new SelectListItem { Text = item.SiteName.ToString(), Value = item.ID.ToString() });
            }
            ViewBag.SiteNameId = SiteDetails;
            return View();

            //Tax
            var listsItem = new SelectList(db.productMasters, "ID", "Tax");
            List<SelectListItem> Tax = new List<SelectListItem>();
            foreach (var items in db.productMasters.ToList())
            {
                Tax.Add(new SelectListItem { Text = items.Tax.ToString(), Value = items.ID.ToString() });
            }
            ViewBag.ProductTax = Tax;
            //product No
            int proNo = 0;
            var productNo = db.issues.Where(p => p.IssueID != null).ToList();

            if (productNo.Count > 0)
            {
                proNo = productNo.Max(x => x.IssueID);
            }
            else proNo = 1;
            ViewBag.IssueID = proNo;
            
            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IssuesDate,ProjectId,SiteId,SiteAddressId,netAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate,GNameId,SiteNameId,IssueID")] Issues issues)
        {

            try
            {
                int invoiceNo = 1;

                issues.CreatedDate = DateTime.UtcNow;
                issues .UpdatedDate = DateTime.UtcNow;

                var issue = db.issues.Where(x => x.isDeleted == false).ToList();
                if (issue != null && issue.Count > 0)
                {
                    invoiceNo = issue.Max(x => x.IssueID );
                    if (invoiceNo != 0)
                    {
                        invoiceNo = invoiceNo + 1;
                    }
                }
                issues.IssueID  = invoiceNo;
                db.issues.Add(issues);


                db.SaveChanges();
                return RedirectToAction("Index", issues .IssueID);
            }
            catch (Exception ex)
            {


                
                ViewBag.GNameId = new SelectList(db.godowns, "Id", "godownName", issues.GNameId);
                ViewBag.SiteNameId = new SelectList(db.siteDetails, "ID", "SiteName",issues.SiteNameId );
                ViewBag.ProjectId = new SelectList(db.siteDetails, "ID", "ProjectName", issues.ProjectId);
                ViewBag.ProductId = new SelectList(db.siteDetails, "ID", "ProjectName", issues.ProjectId);
                ViewBag.SiteId = new SelectList(db.siteDetails, "ID", "SiteName",issues.SiteId);
                ViewBag.SiteAddressId = new SelectList(db.siteDetails, "ID", "SiteAddress", issues.SiteAddressId);

                return View(issues);

            }

        }
            
        [HttpPost]
        public JsonResult SiteId(int site_NameId)
        {
            if (site_NameId > 0)
            {
                var resp = db.siteDetails.Where(x => x.ID == site_NameId).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

      
        [HttpPost]
        public JsonResult TaxId(int tax_Amount)
        {
            if (tax_Amount > 0)
            {
                var resp = db.productMasters.Where(x => x.ID == tax_Amount).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProductVal(int category)
        {
            if (category > 0)
            {
                var resp = db.productMasters.Where(x => x.CategoryId == category).ToList();
                return Json(resp, JsonRequestBehavior.AllowGet);
            }
            else return Json("NoData", JsonRequestBehavior.AllowGet);
        }


        // GET: Issues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issues issues = db.issues.Find(id);
            if (issues == null)
            {
                return HttpNotFound();
            }
           // ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", issues.CategoryId);
            return View(issues);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IssuesDate,ProjectId,SiteId,SiteAddressId,netAmount,isDeleted,CreatedDate,UpdateBy,UpdatedDate,GNameId,SiteNameId,IssueID")] Issues issues)
        {
            if (ModelState.IsValid)
            {
                
                issues.UpdatedDate = DateTime.UtcNow;
                db.Entry(issues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", issues.CategoryId);
            return View(issues);
        }

        // GET: Issues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Issues issues = db.issues.Find(id);
            if (issues == null)
            {
                return HttpNotFound();
            }
            return View(issues);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Issues issues = db.issues.Find(id);
            db.issues.Remove(issues);
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

        [HttpPost]
        public JsonResult savePurchages(IssueTable issueTable)
        {
            try
            {
                int maxValue = 0;
                var isnull = db.issues.Where(x => x.ID != null).ToList();
                if (isnull.Count == 0)
                {
                    maxValue = 1;
                }
                else
                {
                    maxValue = db.issues.Max(x => x.ID );
                    maxValue += 1;

                }
                issueTable.issueId = maxValue;
                issueTable.CreatedDate = DateTime.UtcNow;
                issueTable.UpdatedDate = DateTime.UtcNow;
                issueTable.UpdateBy = Display.Name;
                db.issueTables.Add(issueTable);
                db.SaveChanges();
                var id = db.issueTables.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                return Json(id.ID, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {


            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult removePurchages(string Id, int? InNo)
        {
            try
            {

                if (!string.IsNullOrEmpty(Id) && Id != "undefined")
                {
                    int maxId = db.issues.Max(x => x.ID);
                    if (maxId != null && maxId == 0)
                    {
                        maxId = 1;
                    }
                    else maxId += 1;
                    int pId = int.Parse(Id);
                    var p = db.issueTables.Where(x => x.ID == pId && x.isDeleted == false).FirstOrDefault();
                    db.issueTables.Remove(p);
                    db.SaveChanges();
                    var resp = db.issueTables.Where(x => x.issueId == maxId && x.isDeleted == false).ToList();
                    return Json(resp, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }
            return Json("data", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getListOfPurchaes(int invoice)
        {
            try
            {

                List<IssueTable> issueTables = db.issueTables.Where(x => x.issueId == invoice).ToList();
                return Json(issueTables, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json("data", JsonRequestBehavior.AllowGet);
            }

        }
    }
}
