namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJoinProvinceCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProvinceCustomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProvinceId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.ProvinceId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProvinceCustomers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ProvinceCustomers", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.ProvinceCustomers", new[] { "CustomerId" });
            DropIndex("dbo.ProvinceCustomers", new[] { "ProvinceId" });
            DropTable("dbo.ProvinceCustomers");
        }
    }
}
