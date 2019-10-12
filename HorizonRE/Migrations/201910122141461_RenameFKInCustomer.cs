namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFKInCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerProvinceId", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "ProvinceCustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ProvinceCustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "CustomerProvinceId");
        }
    }
}
