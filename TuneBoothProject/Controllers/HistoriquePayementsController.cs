using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuneBoothProject.Models;

namespace TuneBoothProject.Controllers
{
    public class HistoriquePayementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: HistoriquePayements
        public ActionResult Index()
        {
            return View(db.HistoriquePayements.ToList());
        }

        // GET: HistoriquePayements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriquePayement historiquePayement = db.HistoriquePayements.Find(id);
            if (historiquePayement == null)
            {
                return HttpNotFound();
            }
            return View(historiquePayement);
        }

        // GET: HistoriquePayements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoriquePayements/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,TuneID")] HistoriquePayement historiquePayement)
        {
            if (ModelState.IsValid)
            {
                db.HistoriquePayements.Add(historiquePayement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(historiquePayement);
        }

        // GET: HistoriquePayements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriquePayement historiquePayement = db.HistoriquePayements.Find(id);
            if (historiquePayement == null)
            {
                return HttpNotFound();
            }
            return View(historiquePayement);
        }

        // POST: HistoriquePayements/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,TuneID")] HistoriquePayement historiquePayement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(historiquePayement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(historiquePayement);
        }

        // GET: HistoriquePayements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HistoriquePayement historiquePayement = db.HistoriquePayements.Find(id);
            if (historiquePayement == null)
            {
                return HttpNotFound();
            }
            return View(historiquePayement);
        }

        // POST: HistoriquePayements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HistoriquePayement historiquePayement = db.HistoriquePayements.Find(id);
            db.HistoriquePayements.Remove(historiquePayement);
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
