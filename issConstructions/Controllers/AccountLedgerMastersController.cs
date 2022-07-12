using issConstructions.Custom;
using issConstructions.Models;
using issDomain.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace issConstructions.Controllers
{
   // [CustomAuthorize(Roles = "Admin,Manager")]
    public class AccountLedgerMastersController : Controller
    {
        private issDB db = new issDB();
        // GET: AccountLedgerMasters
        public ActionResult Index()
        {
            return View(db.accountLedgerMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }
        // GET: AccountLedgerMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountLedgerMaster accountLedgerMaster = db.accountLedgerMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (accountLedgerMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountLedgerMaster);
        }
        // GET: AccountLedgerMasters/Create
        public ActionResult Create()
        {
            ViewBag.AccountGroupID = new SelectList(db.accountGroupMasters.Where(x => x.isDeleted == false), "ID", "GroupName");
            ViewBag.AccountLedger = "";
            return View();
        }
        // POST: AccountLedgerMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AccountLedger,AccountGroupID,OpeningBal,Type,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] AccountLedgerMaster accountLedgerMaster)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.accountLedgerMasters.Where(x => x.AccountLedger == accountLedgerMaster.AccountLedger).FirstOrDefault();
                if (duplicate == null)
                {
                    accountLedgerMaster.CreatedDate = DateTime.UtcNow;
                    accountLedgerMaster.UpdatedDate = DateTime.UtcNow;
                    db.accountLedgerMasters.Add(accountLedgerMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    ViewBag.AccountLedger = "Already Exists....!";
                }
            }
            ViewBag.AccountGroupID = new SelectList(db.accountGroupMasters, "ID", "GroupName", accountLedgerMaster.AccountGroupID);
            return View(accountLedgerMaster);
        }
        // GET: AccountLedgerMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountLedgerMaster accountLedgerMaster = db.accountLedgerMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (accountLedgerMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountGroupID = new SelectList(db.accountGroupMasters, "ID", "GroupName", accountLedgerMaster.AccountGroupID);
            return View(accountLedgerMaster);
        }
        // POST: AccountLedgerMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AccountLedger,AccountGroupID,OpeningBal,Type,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] AccountLedgerMaster accountLedgerMaster)
        {
            if (ModelState.IsValid)
            {
                accountLedgerMaster.UpdatedDate = DateTime.UtcNow;
                accountLedgerMaster.CreatedDate = DateTime.UtcNow;
                db.Entry(accountLedgerMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountGroupID = new SelectList(db.accountGroupMasters, "ID", "GroupName", accountLedgerMaster.AccountGroupID);
            return View(accountLedgerMaster);
        }
        // GET: AccountLedgerMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountLedgerMaster accountLedgerMaster = db.accountLedgerMasters.Find(id);
            if (accountLedgerMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountLedgerMaster);
        }
        // POST: AccountLedgerMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountLedgerMaster accountLedgerMaster = db.accountLedgerMasters.Find(id);
            accountLedgerMaster.isDeleted = true;
            db.accountLedgerMasters.Remove(accountLedgerMaster);
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