using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuneBooth.Models;
using TuneBoothProject.Models;

namespace TuneBoothProject.Controllers
{
    public class MagasinController : Controller
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
            if (!String.IsNullOrEmpty(search))
            {
                foreach (Tune tune in db.Tunes.ToList())
                {
                    ViewBag.Search = search;
                    if (tune.Titre.Contains(search))
                    {
                        tunes.Add(tune);
                    }
                }
            }
            return View(tunes); 
        }
    }
}