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
    public class AccountGroupMastersController : Controller
    {
        private issDB db = new issDB();

        // GET: AccountGroupMasters
        public ActionResult Index()
        {
            return View(db.accountGroupMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: AccountGroupMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountGroupMaster accountGroupMaster = db.accountGroupMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (accountGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountGroupMaster);
        }

        // GET: AccountGroupMasters/Create
        public ActionResult Create()
        {
            ViewBag.GroupName = "";
            return View();
        }

        // POST: AccountGroupMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GroupName,ParentGroup,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] AccountGroupMaster accountGroupMaster)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.accountGroupMasters.Where(x => x.GroupName == accountGroupMaster.GroupName).FirstOrDefault();
                if (duplicate == null)
                {
                    accountGroupMaster.CreatedDate = DateTime.UtcNow;
                
                    db.accountGroupMasters.Add(accountGroupMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.GroupName = "Already Exists....!";
                }
                
            }

            return View(accountGroupMaster);
        }

        // GET: AccountGroupMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountGroupMaster accountGroupMaster = db.accountGroupMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (accountGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountGroupMaster);
        }

        // POST: AccountGroupMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GroupName,ParentGroup,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] AccountGroupMaster accountGroupMaster)
        {
            if (ModelState.IsValid)
            {
                accountGroupMaster.UpdatedDate = DateTime.UtcNow;
                accountGroupMaster.CreatedDate = DateTime.UtcNow;
                db.Entry(accountGroupMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountGroupMaster);
        }

        // GET: AccountGroupMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountGroupMaster accountGroupMaster = db.accountGroupMasters.Find(id);
            if (accountGroupMaster == null)
            {
                return HttpNotFound();
            }
            return View(accountGroupMaster);
        }

        // POST: AccountGroupMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountGroupMaster accountGroupMaster = db.accountGroupMasters.Find(id);
            accountGroupMaster.isDeleted = true;
            db.Entry(accountGroupMaster).State = EntityState.Modified;
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
