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
    public class CompanyDetailsController : Controller
    {
        private issDB db = new issDB();

        // GET: CompanyDetails
        public ActionResult Index()
        {
            return View(db.companyDetails.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: CompanyDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetails companyDetails = db.companyDetails.Where (x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (companyDetails == null)
            {
                return HttpNotFound();
            }
            return View(companyDetails);
        }

        // GET: CompanyDetails/Create
        public ActionResult Create()
        {
            ViewBag.NameoftheCompany = "";
            return View();
        }

        // POST: CompanyDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NameoftheCompany,PrintName,Address,City,State,EmailID,PhoneNumber,GSTNumber,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] CompanyDetails companyDetails)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.companyDetails.Where(x => x.NameoftheCompany == companyDetails.NameoftheCompany && x.Address == companyDetails.Address).FirstOrDefault();
                if (duplicate == null)
                {
                    companyDetails.CreatedDate = DateTime.UtcNow;
                    companyDetails.UpdatedDate = DateTime.UtcNow;
                    db.companyDetails.Add(companyDetails);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.NameoftheCompany = "Already Exists....!";
                }
            }

            return View(companyDetails);
        }

        // GET: CompanyDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetails companyDetails = db.companyDetails.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (companyDetails == null)
            {
                return HttpNotFound();
            }
            return View(companyDetails);
        }

        // POST: CompanyDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameoftheCompany,PrintName,Address,City,State,EmailID,PhoneNumber,GSTNumber,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] CompanyDetails companyDetails)
        {
            if (ModelState.IsValid)
            {
                companyDetails.UpdatedDate = DateTime.UtcNow;
                companyDetails.CreatedDate = DateTime.UtcNow;
                db.Entry(companyDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyDetails);
        }

        // GET: CompanyDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDetails companyDetails = db.companyDetails.Find(id);
            if (companyDetails == null)
            {
                return HttpNotFound();
            }
            return View(companyDetails);
        }

        // POST: CompanyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyDetails companyDetails = db.companyDetails.Find(id);
            companyDetails.isDeleted = true;
            db.companyDetails.Remove(companyDetails);
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
