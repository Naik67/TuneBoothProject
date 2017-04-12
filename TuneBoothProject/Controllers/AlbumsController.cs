using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    [Authorize]
    public class AlbumsController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Albums
        public ActionResult Index()
        {
            ModelsVM vm = new ModelsVM();
            vm.Albums = db.Albums.ToList();
            vm.Artistes = db.Artistes.ToList();
            vm.Tunes = db.Tunes.ToList();
            return View(vm);
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.artiste = db.Artistes.Find(album.ArtisteID);
            ViewBag.tunes = db.Tunes.ToList();
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Titre,ArtisteID,DateSortie,Genre,Prix")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Titre,ArtisteID,DateSortie,Genre,Prix")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Purchase(int id)
        {
            List<Tune> ltunes = new List<Tune>();
            foreach (var tune in db.Tunes.ToList())
            {
                if (tune.AlbumID == id)
                {
                    ltunes.Add(tune);
                }
            }
            ViewBag.tunes = ltunes;
            ViewBag.album = db.Albums.Find(id);
            TempData["albumID"] = id;
            return View();
        }

        public ActionResult ConfirmPurchase()
        {
            int albumid = Int32.Parse(TempData["albumID"].ToString());
            foreach(var tune in db.Tunes.ToList())
            {
                if(tune.AlbumID == albumid)
                {
                    HistoriquePayement hp = new HistoriquePayement();
                    hp.UserID = User.Identity.GetUserId();
                    hp.TuneID = tune.ID;
                    db.HistoriquePayements.Add(hp);
                }
            }
            db.SaveChanges();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

    }
}
