using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuneBoothProject.Models
{
    public class PlaylistCompo
    {
        [Key]
        public int ID { get; set; }
        public int PlaylistID { get; set; }
        public int TuneID { get; set; }

        public PlaylistCompo()
        {

        }

        public PlaylistCompo(int id, int playlistid, int tuneid)
        {
            this.ID = id;
            this.PlaylistID = playlistid;
            this.TuneID = tuneid;
        }
    }
}