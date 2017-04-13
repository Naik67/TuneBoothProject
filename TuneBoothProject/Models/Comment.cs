using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuneBoothProject.Models
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public int TuneID { get; set; }
        public string Content { get; set; }

        public Comment()
        {

        }

        public Comment(int id, string userid, int tuneid, string content)
        {
            this.ID = id;
            this.UserID = userid;
            this.TuneID = tuneid;
            this.Content = content;
        }
    }
}