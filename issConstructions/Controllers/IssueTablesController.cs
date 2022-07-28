 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issConstructions.Models;
using issDomain.Models;

namespace issConstructions.Controllers
{
    public class IssueTablesController : Controller
    {
        private issDB db = new issDB();

        // GET: IssueTables
        public ActionResult Index(int Id)
        {

            ViewBag.pId = Id;
            var issues = db.issues.Where(x => x.IssueID == Id).FirstOrDefault();
            if (issues != null)
            {
                ViewBag.ProjectName = db.siteDetails.Where(x => x.ID == issues.SiteNameId).FirstOrDefault();
                if (ViewBag.ProjectName == null)
                {
                    ViewBag.ProjectName = "Project 1";

                }
                else ViewBag.ProjectName = ViewBag.ProjectName.SiteName;

            }
            else
            {
                ViewBag.ProjectName = "Project 1";

            }

            //var issueTables = db.issueTables.Include(p => p.Product).Where(x => x.issueId == issues.IssueID).ToList();
            //var totalAmunt = issueTables.Sum(x => x.TotalAmount);

            //var NetAmount = (totalAmunt);
            //var purch = db.issues.Where(x => x.OrderId == Id).FirstOrDefault();
            //purch.NetAmount = (totalAmunt);
            //db.Entry(purch).State = EntityState.Modified;
            //db.SaveChanges();
            //return View(pur.ToList());
            var issueTables = db.issueTables.Include(p => p.Product).Where(x => x.issueId == issues.IssueID).ToList();
            //var issueTables = db.issueTables.Include(i => i.Category).Include(i => i.Product);
            return View(issueTables.ToList());
        }

        // GET: IssueTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTable issueTable = db.issueTables.Find(id);
            if (issueTable == null)
            {
                return HttpNotFound();
            }
            return View(issueTable);
        }

        // GET: IssueTables/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName");
            return View();
        }

        // POST: IssueTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,issueId,productId,CategoryId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] IssueTable issueTable)
        {
            if (ModelState.IsValid)
            {
                db.issueTables.Add(issueTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", issueTable.CategoryId);
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", issueTable.productId);
            return View(issueTable);
        }

        // GET: IssueTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTable issueTable = db.issueTables.Find(id);
            if (issueTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", issueTable.CategoryId);
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", issueTable.productId);
            return View(issueTable);
        }

        // POST: IssueTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,issueId,productId,CategoryId,Description,Tax,Rate,Quantity,Amount,TaxAmount,TotalAmount,ProductNo,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] IssueTable issueTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(issueTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", issueTable.CategoryId);
            ViewBag.productId = new SelectList(db.productMasters, "ID", "ProductName", issueTable.productId);
            return View(issueTable);
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


        // GET: IssueTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IssueTable issueTable = db.issueTables.Find(id);
            if (issueTable == null)
            {
                return HttpNotFound();
            }
            return View(issueTable);
        }

        // POST: IssueTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IssueTable issueTable = db.issueTables.Find(id);
            db.issueTables.Remove(issueTable);
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
