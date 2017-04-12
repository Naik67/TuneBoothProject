using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuneBooth.Models;
using TuneBoothProject.Models;

namespace TuneBoothProject.Controllers
{
    public class MagasinController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Magasin
        public ActionResult Index()
        {
            ViewBag.Tunes = db.Tunes.ToList();
            return View();
        }

        // GET: Magasin/Searchq?=search
        public ActionResult Search(string search)
        {
            List<Tune> tunes = new List<Tune>();
            List<Album> albums = new List<Album>();
            List<Artiste> artistes = new List<Artiste>();
            if (!String.IsNullOrEmpty(search))
            {
                foreach (Tune tune in db.Tunes.ToList())
                {
                    if (tune.Titre.ToLower().Contains(search.ToLower()))
                    {
                        tunes.Add(tune);
                    }
                }
                foreach (Album album in db.Albums.ToList())
                {
                    if (album.Titre.ToLower().Contains(search.ToLower()))
                    {
                        albums.Add(album);
                    }
                }
                foreach (Artiste artiste in db.Artistes.ToList())
                {
                    if (artiste.NomArtiste.ToLower().Contains(search.ToLower()))
                    {
                        artistes.Add(artiste);
                    }
                }
            }
            ViewBag.Search = search;
            ViewBag.tunesearch = tunes;
            ViewBag.albumsearch = albums;
            ViewBag.artistesearch = artistes;
            return View(); 
        }
    }
}