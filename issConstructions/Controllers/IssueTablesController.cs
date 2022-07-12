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
        public ActionResult Index()
        {
            var issueTables = db.issueTables.Include(i => i.Category).Include(i => i.Product);
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
