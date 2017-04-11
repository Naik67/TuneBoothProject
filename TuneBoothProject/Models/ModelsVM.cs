using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TuneBooth.Models;
using TuneBoothProject.Models;

namespace TuneBooth.Models
{
    public class ModelsVM
    {
        public List<Tune> Tunes { get; set; }
        public List<Album> Albums { get; set; }
        public List<Artiste> Artistes { get; set; }
        public Tune Tune { get; set; }
        public Album Album { get; set; }
        public Artiste Artiste { get; set; }

        public ModelsVM()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            Tunes = new List<Tune>();
            Albums = new List<Album>();
            Artistes = new List<Artiste>();
            Tune = new Tune();
            Album = new Album();
            Artiste = new Artiste();
        }
    }
}