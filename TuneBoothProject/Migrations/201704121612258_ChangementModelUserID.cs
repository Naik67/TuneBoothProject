namespace TuneBoothProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangementModelUserID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HistoriquePayements", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HistoriquePayements", "UserID", c => c.Int(nullable: false));
        }
    }
}
