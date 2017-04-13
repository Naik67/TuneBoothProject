using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuneBoothProject.Models
{
    public class Note
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int TuneID { get; set; }
        public int Value { get; set; }

        public Note()
        {

        }

        public Note(int id, string userid, int tuneid, int value)
        {
            this.ID = id;
            this.UserID = userid;
            this.TuneID = tuneid;
            this.Value = value;
        }
    }
}