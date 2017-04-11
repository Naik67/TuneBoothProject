using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuneBooth.Models
{
    public class Artiste
    {
        [Key]
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomArtiste { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Biographie { get; set; }

        public Artiste()
        {

        }

        public Artiste(int id, string nom, string prenom, string nomartiste, DateTime datenaissance, string biographie)
        {
            this.ID = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.NomArtiste = nomartiste;
            this.DateNaissance = datenaissance;
            this.Biographie = biographie;
        }
    }
}