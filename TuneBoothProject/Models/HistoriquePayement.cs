using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuneBoothProject.Models
{
    public class HistoriquePayement
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int TuneID { get; set; }

        public HistoriquePayement()
        {

        }

        public HistoriquePayement(int id, string userid, int tuneid)
        {
            this.ID = id;
            this.UserID = userid;
            this.TuneID = tuneid;
        }
    }
}