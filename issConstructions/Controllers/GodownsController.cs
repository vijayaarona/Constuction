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
    public class GodownsController : Controller
    {
        private issDB db = new issDB();

        // GET: Godowns
        public ActionResult Index()
        {
            return View(db.godowns.ToList());
        }

        // GET: Godowns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Godown godown = db.godowns.Find(id);
            if (godown == null)
            {
                return HttpNotFound();
            }
            return View(godown);
        }

        // GET: Godowns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Godowns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,godownName,address,authPerson,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] Godown godown)
        {
            if (ModelState.IsValid)
            {
                db.godowns.Add(godown);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(godown);
        }

        // GET: Godowns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Godown godown = db.godowns.Find(id);
            if (godown == null)
            {
                return HttpNotFound();
            }
            return View(godown);
        }

        // POST: Godowns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,godownName,address,authPerson,isDeleted,CreatedDate,UpdateBy,UpdatedDate")] Godown godown)
        {
            if (ModelState.IsValid)
            {
                db.Entry(godown).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(godown);
        }

        // GET: Godowns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Godown godown = db.godowns.Find(id);
            if (godown == null)
            {
                return HttpNotFound();
            }
            return View(godown);
        }

        // POST: Godowns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Godown godown = db.godowns.Find(id);
            db.godowns.Remove(godown);
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
