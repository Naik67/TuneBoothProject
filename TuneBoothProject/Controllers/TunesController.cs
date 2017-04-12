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
    [Authorize]
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
            TempData["tuneID"] = id;
            if (tune == null)
            {
                return HttpNotFound();
            }
            ViewBag.artiste = db.Artistes.Find(tune.ArtisteID);
            ViewBag.album = db.Albums.Find(tune.AlbumID);
            ViewBag.filename = "extract-" + tune.ID + "-" + tune.Titre + "." + tune.Format;
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
                if (ModelState.IsValid)
                {
                    int file = 0;
                    if (!Directory.Exists(Server.MapPath("~/Musiques")))
                        Directory.CreateDirectory(Server.MapPath("~/Musiques"));
                    foreach (string upload in Request.Files)
                    {
                        if (Request.Files[upload].ContentLength == 0) continue;
                        if (IsMediaFile(Request.Files[upload].FileName)) continue;

                        db.Tunes.Add(tune);
                        db.SaveChanges();
                        string pathToSave = Server.MapPath("~/Musiques/");
                        string filename = Path.GetFileName(Request.Files[upload].FileName);
                        string tosave = Path.Combine(pathToSave, +tune.ID + "-" + tune.Titre + Path.GetExtension(Request.Files[upload].FileName));
                        tosave = tosave.Replace(' ', '_');
                        Request.Files[upload].SaveAs(tosave);

                        //Création de l'extrait - @N.
                        //string songname = tune.ID + "-" + tune.Titre + Path.GetExtension(Request.Files[upload].FileName);
                        //var trackDir = Server.MapPath("~/Musiques");
                        //var trackPath = Path.Combine(trackDir, songname);
                        //var ffmpegDir = Server.MapPath("~/ffmpeg");
                        //var ffmpegPath = Path.Combine(ffmpegDir, "bin/ffmpeg.exe");
                        //var trailerDir = Server.MapPath("~/Trailer");
                        //var trackTrailerPath = Path.Combine(trailerDir, "extract-" + songname);
                        //var trackTrailerUrl = Path.Combine(trailerDir, "extract-" + songname);
                        //System.Diagnostics.Process p = new System.Diagnostics.Process();
                        //p.StartInfo = new System.Diagnostics.ProcessStartInfo(ffmpegPath, "-t 30 -i " + trackPath + " " + trackTrailerPath);
                        //p.Start();
                        //p.WaitForExit();


                        TempData["shortMessage"] = Request.Files[upload].FileName;
                        file++;
                    }
                    if (file > 0)
                    {
                        //TempData["shortMessage"] = "Musique ajoutée";
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
                return RedirectToAction("Error");
        }

        // GET: Tunes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = User.Identity;
                ViewBag.Name = user.Name;

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

            //verification si la musique a déjà été achetée
            bool exists = false;
            foreach (var histo in db.HistoriquePayements.ToList())
            {
                if (histo.TuneID == hp.TuneID && histo.UserID == hp.UserID)
                    exists = true;
            }
            if (!exists)
            {
                db.HistoriquePayements.Add(hp);
                db.SaveChanges();
            }
            return View();
        }

        // POST: Tunes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tune tune = db.Tunes.Find(id);
            string sbase = tune.ID + "-" + tune.Titre + "." + tune.Format;
            string file = Path.Combine(Server.MapPath("~/Musiques"), sbase);
            string extract = Path.Combine(Server.MapPath("~/Trailer"), "extract-"+sbase);
            if (System.IO.File.Exists(file))
                System.IO.File.Delete(file);
            if (System.IO.File.Exists(extract))
                System.IO.File.Delete(extract);
            db.Tunes.Remove(tune);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Download(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = User.Identity;
                ViewBag.Name = user.Name;

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Tune tune = db.Tunes.Find(id);
                ViewBag.artiste = db.Artistes.Find(tune.ArtisteID);
                ViewBag.album = db.Albums.Find(tune.AlbumID);
                if (tune == null)
                {
                    return HttpNotFound();
                }
                return View(tune);
            }
            else
                return View("Error");
        }


        [HttpPost, ActionName("Download")]
        [ValidateAntiForgeryToken]
        public ActionResult DownloadValidate(int? id)
        {
            try
            {
                Tune t = db.Tunes.Find(id);
                ViewBag.Message = Server.MapPath("~/Musiques") + t.ID + "-" + t.Titre + t.Format;
                string fullName = Path.Combine(Server.MapPath("~/Musiques"), +t.ID + "-" + t.Titre +"."+ t.Format);
                fullName = fullName.Replace(' ', '_');
                byte[] fileBytes = GetFile(fullName);
                return File(
                    fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, t.Titre + "." + t.Format);
            }
            catch (Exception)
            {
                return Error();
            }
        }
        /*
        public ActionResult Extract(string filename)
        {
            //string filename = "5-Rain.mp3";
            var trackDir = Server.MapPath("~/Musiques");
            var trackPath = Path.Combine(trackDir, filename);
            var ffmpegDir = Server.MapPath("~/ffmpeg");
            var ffmpegPath = Path.Combine(ffmpegDir, "bin/ffmpeg.exe");
            var trailerDir = Server.MapPath("~/Trailer");
            var trackTrailerPath = Path.Combine(trailerDir, "extract-"+filename);
            var trackTrailerUrl = Path.Combine(trailerDir, "extract-"+filename);
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo(ffmpegPath, "-t 30 -i " + trackPath + " " + trackTrailerPath);
            p.Start();
            p.WaitForExit();
            return View();
        }
        */
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
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
