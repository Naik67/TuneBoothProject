namespace TuneBoothProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playlists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Playlists");
        }
    }
}
