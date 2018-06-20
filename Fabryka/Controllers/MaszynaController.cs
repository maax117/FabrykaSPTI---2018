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
    public class MaszynaController : Controller
    {
        private FabrykaContainer db = new FabrykaContainer();

        // GET: Maszyna
        public ActionResult Index(string wyszukaj_maszyne)
        {
            if (string.IsNullOrEmpty(wyszukaj_maszyne))
            {
                var maszynaSet = db.MaszynaSet.Include(m => m.Hala);
                return View(maszynaSet.ToList());
            }
            else
            {
                var maszynaSet = db.MaszynaSet.Include(m => m.Hala).Where(m => m.Nazwa.Contains(wyszukaj_maszyne)).OrderBy(m => m.Nazwa);
                return View(maszynaSet.ToList());
            }
                
        }

        // GET: Maszyna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maszyna maszyna = db.MaszynaSet.Find(id);
            if (maszyna == null)
            {
                return HttpNotFound();
            }
            return View(maszyna);
        }

        // GET: Maszyna/Create
        public ActionResult Create()
        {
            ViewBag.HalaId = new SelectList(db.HalaSet, "Id", "Nazwa");
            return View();
        }

        // POST: Maszyna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nazwa,Data_uruchomienia,HalaId")] Maszyna maszyna)
        {
            if (ModelState.IsValid)
            {
                db.MaszynaSet.Add(maszyna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HalaId = new SelectList(db.HalaSet, "Id", "Nazwa", maszyna.HalaId);
            return View(maszyna);
        }

        // GET: Maszyna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maszyna maszyna = db.MaszynaSet.Find(id);
            if (maszyna == null)
            {
                return HttpNotFound();
            }
            ViewBag.HalaId = new SelectList(db.HalaSet, "Id", "Nazwa", maszyna.HalaId);
            return View(maszyna);
        }

        // POST: Maszyna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nazwa,Data_uruchomienia,HalaId")] Maszyna maszyna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maszyna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HalaId = new SelectList(db.HalaSet, "Id", "Nazwa", maszyna.HalaId);
            return View(maszyna);
        }

        // GET: Maszyna/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maszyna maszyna = db.MaszynaSet.Find(id);
            if (maszyna == null)
            {
                return HttpNotFound();
            }
            return View(maszyna);
        }

        // POST: Maszyna/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Maszyna maszyna = db.MaszynaSet.Find(id);
            db.MaszynaSet.Remove(maszyna);
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
