namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixValidationImage2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ImageFiles", "ImageName", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ImageFiles", "ImageName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
