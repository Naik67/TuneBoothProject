using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuneBoothProject.Models
{
    public class Playlist
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Nom { get; set; }

        public Playlist()
        {

        }

        public Playlist(int id, string userid, string nom)
        {
            this.ID = id;
            this.UserID = userid;
            this.Nom = nom;
        }
    }
}