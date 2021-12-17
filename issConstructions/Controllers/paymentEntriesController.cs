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
    public class paymentEntriesController : Controller
    {
        private issDB db = new issDB();

        // GET: paymentEntries
        public ActionResult Index()
        {
            return View(db.paymentEntries.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: paymentEntries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            if (paymentEntry == null)
            {
                return HttpNotFound();
            }
            return View(paymentEntry);
        }

        // GET: paymentEntries/Create
        public ActionResult Create()
        {
            ViewBag.paymentTypeId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" && x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName");
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName");
            return View();
        }

        // POST: paymentEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,paymentID,paymenttDate,paymentTypeId,accountLedgerNameId,projectNameId,siteNameId,givenBy,collectBy,amount,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] paymentEntry paymentEntry)
        {
            if (ModelState.IsValid)
            {
                db.paymentEntries.Add(paymentEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paymentTypeId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.projectNameId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteNameId);
            return View(paymentEntry);
        }

        // GET: paymentEntries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            if (paymentEntry == null)
            {
                return HttpNotFound();
            }

            ViewBag.paymentTypeId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.projectNameId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteNameId);
            return View(paymentEntry);
        }

        // POST: paymentEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,paymentID,paymenttDate,paymentTypeId,accountLedgerNameId,projectNameId,siteNameId,givenBy,collectBy,amount,remarks,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] paymentEntry paymentEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.paymentTypeId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName == "Cash in Hand" || x.AccountGroup.GroupName == "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.accountLedgerNameId = new SelectList(db.accountLedgerMasters.Where(x => x.AccountGroup.GroupName != "Cash in Hand" || x.AccountGroup.GroupName != "Bank Accounts").ToList(), "ID", "AccountLedger");
            ViewBag.projectNameId = new SelectList(db.siteDetails, "ID", "ProjectName", paymentEntry.projectNameId);
            ViewBag.siteNameId = new SelectList(db.siteDetails, "ID", "SiteName", paymentEntry.siteNameId);
            return View(paymentEntry);
        }

        // GET: paymentEntries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            if (paymentEntry == null)
            {
                return HttpNotFound();
            }
            return View(paymentEntry);
        }

        // POST: paymentEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paymentEntry paymentEntry = db.paymentEntries.Find(id);
            db.paymentEntries.Remove(paymentEntry);
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
