namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreasedImageNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageFiles", "ImageName", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageFiles", "ImageName", c => c.String(maxLength: 100));
        }
    }
}
