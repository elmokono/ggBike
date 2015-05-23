using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikeGoGreen.Models;
using Model;

namespace BikeGoGreen.Controllers
{
    public class RunnersController : Controller
    {
        private Model.DatabaseModelDataContext db = new Model.DatabaseModelDataContext();

        // GET: Runners
        public ActionResult Index()
        {
            return View(db.Runners.ToList());
        }

        // GET: Runners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runner runner = db.Runners.FirstOrDefault(x => x.Id.Equals(id));
            if (runner == null)
            {
                return HttpNotFound();
            }
            return View(runner);
        }

        // GET: Runners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Runners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,EMail")] Runner runner)
        {
            if (ModelState.IsValid)
            {
                db.Runners.InsertOnSubmit(runner);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }

            return View(runner);
        }

        // GET: Runners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runner runner = db.Runners.FirstOrDefault(x => x.Id.Equals(id));
            if (runner == null)
            {
                return HttpNotFound();
            }
            return View(runner);
        }

        // POST: Runners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,EMail")] Runner runner)
        {
            if (ModelState.IsValid)
            {
                var r = db.Runners.FirstOrDefault(x => x.Id.Equals(runner.Id));
                r.FullName = runner.FullName;
                r.EMail = runner.EMail;
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(runner);
        }

        // GET: Runners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Runner runner = db.Runners.FirstOrDefault(x => x.Id.Equals(id));
            if (runner == null)
            {
                return HttpNotFound();
            }
            return View(runner);
        }

        // POST: Runners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Runner runner = db.Runners.FirstOrDefault(x => x.Id.Equals(id));
            db.Runners.DeleteOnSubmit(runner);
            db.SubmitChanges();
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
