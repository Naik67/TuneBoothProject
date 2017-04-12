namespace TuneBoothProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlaylistCompo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlaylistCompoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlaylistID = c.Int(nullable: false),
                        TuneID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlaylistCompoes");
        }
    }
}
