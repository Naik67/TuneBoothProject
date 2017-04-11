using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuneBooth.Models
{
    public class Tune
    {
        [Required]
        public int ID { get; set; }
        public string Titre { get; set; }
        public int ArtisteID { get; set; }
        public double Prix { get; set; }
        public DateTime DateSortie { get; set; }
        public string Genre { get; set; }
        public string Format { get; set; }
        public int AlbumID { get; set; }

        public Tune()
        {
            this.ID = 0;
        }

        public Tune(string titre, int artisteid, double prix, DateTime sortie, string genre, string format, int albumid)
        {
            //todo
            //faire l'id
            //il faut utiliser une fonction qui va get le dernier ID
            this.Titre = titre;
            this.ArtisteID = artisteid;
            this.Prix = prix;
            this.DateSortie = sortie;
            this.Genre = genre;
            this.Format = format;
            this.AlbumID = albumid;
        }

        public string GetLink()
        {
            //TODO à modifier
            return ArtisteID.ToString() + '_' + AlbumID.ToString() + '_' + this.ID.ToString();
        }
    }
}