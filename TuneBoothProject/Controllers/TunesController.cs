using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TuneBooth.Models;
using TuneBoothProject.Models;

namespace TuneBoothProject.Controllers
{
    public class TunesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private static string[] mediaExtensions = {
    ".FLAC", ".ALAC", ".SHORTEN", ".AC-3", ".MP3",
    ".MP3PRO", ".MID", ".MIDI", ".WMA", ".AU", ".OGG", ".RMA",
    ".AA", ".AAC", ".MPEG-2", ".ATRACT",
};
        // GET: Tunes
        public ActionResult Index()
        {

            ViewBag.Message = TempData["shortMessage"];
            ModelsVM vm = new ModelsVM();
            vm.Albums = db.Albums.ToList();
            vm.Artistes = db.Artistes.ToList();
            vm.Tunes = db.Tunes.ToList();
            return View(vm);
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
            ViewBag.artiste = db.Artistes.Find(tune.ArtisteID);
            ViewBag.album = db.Albums.Find(tune.AlbumID);
            return View(tune);
        }

        // GET: Tunes/Create
        public ActionResult Create()
        {
            ViewBag.Message = "get";
            return View();
        }



        static bool IsMediaFile(string path)
        {
            return !(-1 != Array.IndexOf(mediaExtensions, Path.GetExtension(path).ToUpperInvariant()));
        }

        // POST: Tunes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Include = "ID,Titre,ArtisteID,Prix,DateSortie,Genre,Format,AlbumID")] Tune tune)
        {
            ViewBag.Message = "début";
            if (User.Identity.IsAuthenticated)
            {
                if (!isAdminUser())
                {
                    if (ModelState.IsValid)
                    {
                        int file = 0;
                        if (!Directory.Exists(Server.MapPath("~/Musiques")))
                            Directory.CreateDirectory(Server.MapPath("~/Musiques"));
                        foreach (string upload in Request.Files)
                        {
                            if (Request.Files[upload].ContentLength == 0) continue;
                            if (IsMediaFile(Request.Files[upload].FileName)) continue;

                            string pathToSave = Server.MapPath("~/Musiques/");
                            string filename = Path.GetFileName(Request.Files[upload].FileName);
                            Request.Files[upload].SaveAs(Path.Combine(pathToSave, +tune.ID+"-"+tune.Titre + Path.GetExtension(Request.Files[upload].FileName)));
                            file++;
                            TempData["shortMessage"] = Request.Files[upload].FileName;
                        }
                        if(file>0)
                        {
                            //TempData["shortMessage"] = "Musique ajoutée";
                            db.Tunes.Add(tune);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["shortMessage"] = "Fichier manquant ou au mauvais format";
                            ViewBag.Message = "Fichier manquant";
                            return RedirectToAction("Index");
                        }
                    }

                    return View(tune);
                }
                else
                    return View("Error");
            }
            else
                return View("Error");
        }

        // GET: Tunes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (isAdminUser())
                {
                    ViewBag.displayMenu = "Yes";

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
                else
                    return View("Error");
            }
            else
                return View("Error");
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

        public ActionResult Purchase(int id)
        {
            ViewBag.tune = db.Tunes.Find(id);
            TempData["tuneID"] = id;
            return View();
        }

        public ActionResult ConfirmPurchase()
        {
            int tuneid = Int32.Parse(TempData["tuneID"].ToString());
            HistoriquePayement hp = new HistoriquePayement();
            hp.UserID = User.Identity.GetUserId();
            hp.TuneID = tuneid;
            db.HistoriquePayements.Add(hp);
            db.SaveChanges();
            return View();
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
