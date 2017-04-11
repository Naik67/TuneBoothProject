using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuneBooth.Models
{
    public class Album
    {
        [Key]
        public int ID { get; set; }
        public string Titre { get; set; }
        public int ArtisteID { get; set; }
        public DateTime DateSortie { get; set; }
        public string Genre { get; set; }
        public double Prix { get; set; }

        public Album()
        {

        }

        public Album(int id, string titre, int artisteid, DateTime datesortie, string genre, double prix)
        {
            this.ID = id;
            this.Titre = titre;
            this.ArtisteID = artisteid;
            this.DateSortie = datesortie;
            this.Genre = genre;
            this.Prix = prix;
        }
    }
}