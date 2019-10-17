namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixValidationImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageFiles", "Path", c => c.String());
            AlterColumn("dbo.ImageFiles", "AltText", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageFiles", "AltText", c => c.String(nullable: false));
            AlterColumn("dbo.ImageFiles", "Path", c => c.String(nullable: false));
        }
    }
}
