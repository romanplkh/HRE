namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ProvinceCustomerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ProvinceCustomerId");
        }
    }
}
