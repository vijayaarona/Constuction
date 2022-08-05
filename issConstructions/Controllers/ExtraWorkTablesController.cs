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
    public class ExtraWorkTablesController : Controller
    {
        private issDB db = new issDB();

        // GET: ExtraWorkTables
        public ActionResult Index(int Id)
        {
            ViewBag.pId = Id;
            var extrawork = db.extraWorks.Where(x => x.ExtraWorkId == Id).FirstOrDefault();

            var extraWorkTables = db.extraWorkTables.Where(x => x.extraWorkId  == extrawork.ExtraWorkId).ToList();
            var totalAmunt = extraWorkTables.Sum(x => x.amount);
            var NetAmount = (totalAmunt);
            var purch = db.extraWorks.Where(x => x.ExtraWorkId == Id).FirstOrDefault();

            db.Entry(purch).State = EntityState.Modified;
            db.SaveChanges();
            return View(extraWorkTables.ToList());
        }

        // GET: ExtraWorkTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWorkTable extraWorkTable = db.extraWorkTables.Find(id);
            if (extraWorkTable == null)
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

            return View(extraWorkTable);
        }

        // GET: ExtraWorkTables/Create
        public ActionResult Create(int id)
        {
            ViewBag.PId = id;
            ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM");
            return View();
        }

        // POST: ExtraWorkTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,extraWorkId,Description,length,Breath,Deapth,Nos,quantity,unitPrice,amount,umoId,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ExtraWorkTable extraWorkTable)
        {
            try
            {

                db.extraWorkTables.Add(extraWorkTable);
                db.SaveChanges();
                return RedirectToAction("Index", "ExtraWorkTables", new { Id = extraWorkTable.extraWorkId });
            }
            catch (Exception ex)
            {
                ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM", extraWorkTable.extraWorkId);
                return View(extraWorkTable);
            }
        }

        // GET: ExtraWorkTables/Edit/5
        public ActionResult Edit(int? id)
        {
            //Uom
            var listsItem = new SelectList(db.productMasters, "ID", "UOM");
            List<SelectListItem> Uom = new List<SelectListItem>();
            foreach (var items in db.productMasters.ToList())
            {
                Uom.Add(new SelectListItem { Text = items.UOM.ToString(), Value = items.ID.ToString() });
            }
            ViewBag.umoId = Uom;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWorkTable extraWorkTable = db.extraWorkTables.Find(id);
            if (extraWorkTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.umoId = new SelectList(db.productMasters, "ID", "UOM", extraWorkTable.umoId);
            return View(extraWorkTable);
        }

        // POST: ExtraWorkTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,extraWorkId,Description,length,Breath,Deapth,Nos,quantity,unitPrice,amount,umoId,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ExtraWorkTable extraWorkTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraWorkTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "ExtraWorkTables", new { Id = extraWorkTable.extraWorkId });
            }
            ViewBag.umoId = new SelectList(db.productMasters, "ID", "ProductName", extraWorkTable.umoId);
            return View(extraWorkTable);
        }

        // GET: ExtraWorkTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraWorkTable extraWorkTable = db.extraWorkTables.Find(id);
            if (extraWorkTable == null)
            {
                return HttpNotFound();
            }
            return View(extraWorkTable);
        }

        // POST: ExtraWorkTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraWorkTable extraWorkTable = db.extraWorkTables.Find(id);
            db.extraWorkTables.Remove(extraWorkTable);
            db.SaveChanges();
            return RedirectToAction("Index", "ExtraWorkTables", new { Id =  extraWorkTable .extraWorkId });
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
