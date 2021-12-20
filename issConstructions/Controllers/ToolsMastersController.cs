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
    public class ToolsMastersController : Controller
    {
        private issDB db = new issDB();

        // GET: ToolsMasters
        public ActionResult Index()
        {
            return View(db.toolsMasters.Where(x => x.isDeleted == false).ToList().OrderByDescending(x => x.ID));
        }

        // GET: ToolsMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolsMaster toolsMaster = db.toolsMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (toolsMaster == null)
            {
                return HttpNotFound();
            }
            return View(toolsMaster);
        }

        // GET: ToolsMasters/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName");
            ViewBag.ToolsName = "";
            return View();
        }

        // POST: ToolsMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ToolsName,UOM,CategoryId,openingStock,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ToolsMaster toolsMaster)
        {
            if (ModelState.IsValid)
            {
                var duplicate = db.toolsMasters.Where(x => x.ToolsName == toolsMaster.ToolsName).FirstOrDefault();
                if (duplicate == null)
                {
                    toolsMaster.CreatedDate = DateTime.UtcNow;
                    toolsMaster.UpdatedDate = DateTime.UtcNow;
                    db.toolsMasters.Add(toolsMaster);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ToolsName = "Already Exists....!";
                }
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", toolsMaster.CategoryId);
            return View(toolsMaster);
        }

        // GET: ToolsMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolsMaster toolsMaster = db.toolsMasters.Where(x => x.isDeleted == false && x.ID == id).FirstOrDefault();
            if (toolsMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", toolsMaster.CategoryId);
            return View(toolsMaster);
        }

        // POST: ToolsMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ToolsName,UOM,CategoryId,openingStock,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] ToolsMaster toolsMaster)
        {
            if (ModelState.IsValid)
            {
                toolsMaster.UpdatedDate = DateTime.UtcNow;
                toolsMaster.CreatedDate = DateTime.UtcNow;
                db.Entry(toolsMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categoryMasters, "ID", "CategoryName", toolsMaster.CategoryId);
            return View(toolsMaster);
        }

        // GET: ToolsMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToolsMaster toolsMaster = db.toolsMasters.Find(id);
            if (toolsMaster == null)
            {
                return HttpNotFound();
            }
            return View(toolsMaster);
        }

        // POST: ToolsMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ToolsMaster toolsMaster = db.toolsMasters.Find(id);
            toolsMaster.isDeleted = true;
            db.toolsMasters.Remove(toolsMaster);
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
