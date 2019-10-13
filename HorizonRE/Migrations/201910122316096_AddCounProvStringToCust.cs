namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCounProvStringToCust : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Country", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Province", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Province");
            DropColumn("dbo.Customers", "Country");
        }
    }
}
