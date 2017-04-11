using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuneBooth.Models;
using TuneBoothProject.Models;

namespace TuneBoothProject.Controllers
{
    public class TunesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tunes
        public ActionResult Index()
        {
            return View(db.Tunes.ToList());
        }

        // GET: Tunes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tune tune = db.Tunes.Find(id);
            if (tune == null)
            {
                return HttpNotFound();
            }
            return View(tune);
        }

        // GET: Tunes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tunes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titre,ArtisteID,Prix,DateSortie,Genre,Format,AlbumID")] Tune tune)
        {
            if (ModelState.IsValid)
            {
                db.Tunes.Add(tune);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tune);
        }

        // GET: Tunes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tune tune = db.Tunes.Find(id);
            if (tune == null)
            {
                return HttpNotFound();
            }
            return View(tune);
        }

        // POST: Tunes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titre,ArtisteID,Prix,DateSortie,Genre,Format,AlbumID")] Tune tune)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tune).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tune);
        }

        // GET: Tunes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tune tune = db.Tunes.Find(id);
            if (tune == null)
            {
                return HttpNotFound();
            }
            return View(tune);
        }

        // POST: Tunes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tune tune = db.Tunes.Find(id);
            db.Tunes.Remove(tune);
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
