using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Fabryka;

namespace Fabryka.Controllers
{
    public class OperatorController : Controller
    {
        private FabrykaContainer db = new FabrykaContainer();

        // GET: Operator
        public ActionResult Index()
        {
            return View(db.OperatorSet.ToList());
        }

        // GET: Operator/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = db.OperatorSet.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }

        // GET: Operator/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Operator/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Imie,Nazwisko,Placa")] Operator @operator)
        {
            if (ModelState.IsValid)
            {
                db.OperatorSet.Add(@operator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@operator);
        }

        // GET: Operator/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = db.OperatorSet.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }

        // POST: Operator/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Placa")] Operator @operator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@operator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@operator);
        }

        // GET: Operator/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = db.OperatorSet.Find(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }

        // POST: Operator/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Operator @operator = db.OperatorSet.Find(id);
            db.OperatorSet.Remove(@operator);
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
