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
    public class EmployeeMastersController : Controller
    {
        private issDB db = new issDB();

        // GET: EmployeeMasters
        public ActionResult Index()
        {
            var employeeMaster = db.employeeMaster.Include(e => e.Category).Include(e => e.Designation);
            return View(employeeMaster.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: EmployeeMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMaster employeeMaster = db.employeeMaster.Where (x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (employeeMaster == null)
            {
                return HttpNotFound();
            }
            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.DesignationId = new SelectList(db.designationMasters, "ID", "DesignationName");
            return View();
        }

        // POST: EmployeeMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,address,Emailid,Mobilenumber,Status,SalaryType,SalaryAmount,City,DesignationId,CategoryId,DateofJoin,DateofRelive,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] EmployeeMaster employeeMaster)
        {
            if (ModelState.IsValid)
            {
                employeeMaster.CreatedDate = DateTime.UtcNow;
                employeeMaster.UpdatedDate = DateTime.UtcNow;
                db.employeeMaster.Add(employeeMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", employeeMaster.CategoryId);
            ViewBag.DesignationId = new SelectList(db.designationMasters, "ID", "DesignationName", employeeMaster.DesignationId);
            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMaster employeeMaster = db.employeeMaster.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (employeeMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", employeeMaster.CategoryId);
            ViewBag.DesignationId = new SelectList(db.designationMasters, "ID", "DesignationName", employeeMaster.DesignationId);
            return View(employeeMaster);
        }

        // POST: EmployeeMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,address,Emailid,Mobilenumber,Status,SalaryType,SalaryAmount,City,DesignationId,CategoryId,DateofJoin,DateofRelive,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] EmployeeMaster employeeMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeMaster).State = EntityState.Modified;
                employeeMaster.UpdatedDate = DateTime.UtcNow;
                employeeMaster.CreatedDate = DateTime.UtcNow;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", employeeMaster.CategoryId);
            ViewBag.DesignationId = new SelectList(db.designationMasters, "ID", "DesignationName", employeeMaster.DesignationId);
            return View(employeeMaster);
        }

        // GET: EmployeeMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeMaster employeeMaster = db.employeeMaster.Find(id);
            if (employeeMaster == null)
            {
                return HttpNotFound();
            }
            return View(employeeMaster);
        }

        // POST: EmployeeMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeMaster employeeMaster = db.employeeMaster.Find(id);
            employeeMaster.isDeleted = true;
            db.employeeMaster.Remove(employeeMaster);
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
