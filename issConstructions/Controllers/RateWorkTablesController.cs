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
    public class RateWorkTablesController : Controller
    {
        private issDB db = new issDB();

        // GET: RateWorkTables
        public ActionResult Index(int Id)
        {
            ViewBag.pId = Id;
            var ratework = db.rateWorks.Where(x =>x.RateWorkId == Id).FirstOrDefault();

            var rateWorkTables = db.rateWorkTables.Where(x => x.rateId == ratework.RateWorkId).ToList();
            var totalAmunt = rateWorkTables.Sum(x => x.amount);
            var DiscPercent = ratework.deduction;
            var NetAmount = (totalAmunt - DiscPercent);
            var purch = db.rateWorks.Where(x => x.RateWorkId == Id).FirstOrDefault();

            db.Entry(purch).State = EntityState.Modified;
            db.SaveChanges();
            return View(rateWorkTables.ToList());

        }

        // GET: RateWorkTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateWorkTable rateWorkTable = db.rateWorkTables.Find(id);
            if (rateWorkTable == null)
            {
                return HttpNotFound();
            }
            //Product
            var listItems = new SelectList(db.productMasters, "ID", "UOM");
            List<SelectListItem> Uom = new List<SelectListItem>();
            foreach (var item in db.productMasters.ToList())
            {
                Uom.Add(new SelectListItem { Text = item.UOM, Value = item.ID.ToString() });
            }
            ViewBag.umoId = Uom;

            return View(rateWorkTable);
        }

        // GET: RateWorkTables/Create
        public ActionResult Create(int id)
        {

            ViewBag.PId = id;
            ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM");
            return View();
        }

        // POST: RateWorkTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,rateId,Description,length,Breath,Deapth,Nos,quantity,unitPrice,amount,umoId,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] RateWorkTable rateWorkTable)
        {
            try
            {

                db.rateWorkTables.Add(rateWorkTable);
                db.SaveChanges();
                return RedirectToAction("Index", "rateWorkTables", new { Id = rateWorkTable.rateId });
            }
            catch (Exception ex)
            {
                ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM", rateWorkTable.rateId);
                return View(rateWorkTable);
            }
        }

        // GET: RateWorkTables/Edit/5
        public ActionResult Edit(int? id)
        {

            //Uom
            var listsItem = new SelectList(db.productMasters, "ID", "UOM");
            List<SelectListItem> Uom = new List<SelectListItem>();
            foreach (var items in db.productMasters.ToList())
            {
                Uom.Add(new SelectListItem { Text = items.UOM.ToString(), Value = items.ID.ToString() });
            }
            ViewBag.umoId = Uom ;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateWorkTable rateWorkTable = db.rateWorkTables.Find(id);
            if (rateWorkTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM", rateWorkTable.umoId);
            return View(rateWorkTable);
        }

        // POST: RateWorkTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,rateId,Description,length,Breath,Deapth,Nos,quantity,unitPrice,amount,umoId,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] RateWorkTable rateWorkTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rateWorkTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "RateWorkTables", new { Id = rateWorkTable.rateId });
            }
             ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM", rateWorkTable.umoId);
            return View(rateWorkTable);
        }

        // GET: RateWorkTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RateWorkTable rateWorkTable = db.rateWorkTables.Find(id);
            if (rateWorkTable == null)
            {
                return HttpNotFound();
            }
            return View(rateWorkTable);
        }

        // POST: RateWorkTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RateWorkTable rateWorkTable = db.rateWorkTables.Find(id);
            db.rateWorkTables.Remove(rateWorkTable);
            db.SaveChanges();
            return RedirectToAction("Index", "RateWorkTables", new { Id = rateWorkTable.rateId });
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
