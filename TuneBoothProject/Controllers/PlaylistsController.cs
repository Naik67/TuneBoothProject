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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TuneBoothProject.Controllers
{
    [Authorize]
    public class PlaylistsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Playlists
        public ActionResult Index()
        {
            ModelsVM vm = new ModelsVM();
            vm.Tunes = db.Tunes.ToList();
            vm.UserID = User.Identity.GetUserId();
            foreach (var pl in db.Playlists.ToList())
            {
                if (pl.UserID == User.Identity.GetUserId())
                {
                    vm.Playlists.Add(pl);
                }
            }
            return View(vm);
        }

        // GET: Playlists/AllTunes
        public ActionResult AllTunes()
        {
            ModelsVM vm = new ModelsVM();
            vm.UserID = User.Identity.GetUserId();
            foreach (var hp in db.HistoriquePayements.ToList())
            {
                if (hp.UserID == User.Identity.GetUserId())
                {
                    vm.Tunes.Add(db.Tunes.Find(hp.TuneID));
                }
            }
            return View(vm);
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Playlists/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Nom")] Playlist playlist)
        {
            playlist.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Playlists.Add(playlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(playlist);
        }

        // GET: Playlists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Nom")] Playlist playlist)
        {
            playlist.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(playlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        // GET: Playlists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return View(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            db.Playlists.Remove(playlist);
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
