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
    [Authorize]
    public class PlaylistCompoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlaylistCompoes
        public ActionResult Index()
        {
            return View(db.PlaylistCompoes.ToList());
        }

        // GET: PlaylistCompoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistCompo playlistCompo = db.PlaylistCompoes.Find(id);
            if (playlistCompo == null)
            {
                return HttpNotFound();
            }
            return View(playlistCompo);
        }

        // GET: PlaylistCompoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlaylistCompoes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PlaylistID,TuneID")] PlaylistCompo playlistCompo)
        {
            if (ModelState.IsValid)
            {
                db.PlaylistCompoes.Add(playlistCompo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(playlistCompo);
        }

        // GET: PlaylistCompoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistCompo playlistCompo = db.PlaylistCompoes.Find(id);
            if (playlistCompo == null)
            {
                return HttpNotFound();
            }
            return View(playlistCompo);
        }

        // POST: PlaylistCompoes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PlaylistID,TuneID")] PlaylistCompo playlistCompo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playlistCompo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlistCompo);
        }

        // GET: PlaylistCompoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistCompo playlistCompo = db.PlaylistCompoes.Find(id);
            if (playlistCompo == null)
            {
                return HttpNotFound();
            }
            return View(playlistCompo);
        }

        // POST: PlaylistCompoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlaylistCompo playlistCompo = db.PlaylistCompoes.Find(id);
            db.PlaylistCompoes.Remove(playlistCompo);
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
