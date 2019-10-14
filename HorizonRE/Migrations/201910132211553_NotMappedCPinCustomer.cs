namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotMappedCPinCustomer : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "Country");
            DropColumn("dbo.Customers", "Province");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Province", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Country", c => c.String(nullable: false));
        }
    }
}
