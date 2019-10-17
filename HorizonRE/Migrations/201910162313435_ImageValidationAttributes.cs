namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageValidationAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageFiles", "ImageName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ImageFiles", "ImageDescription", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.ImageFiles", "Path", c => c.String(nullable: false));
            AlterColumn("dbo.ImageFiles", "AltText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageFiles", "AltText", c => c.String());
            AlterColumn("dbo.ImageFiles", "Path", c => c.String());
            AlterColumn("dbo.ImageFiles", "ImageDescription", c => c.String());
            AlterColumn("dbo.ImageFiles", "ImageName", c => c.String());
        }
    }
}
