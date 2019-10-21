namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ListingId = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(),
                        City = c.String(),
                        Province = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(),
                        Area = c.String(),
                        Bedrooms = c.String(),
                        Bathrooms = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractSigned = c.Boolean(nullable: false),
                        ListingStartDate = c.DateTime(nullable: true),
                        ListingEndDate = c.DateTime(nullable: true),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.ListingId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId);
            
            AddColumn("dbo.ImageFiles", "ListingId", c => c.Int(nullable: true));
            CreateIndex("dbo.ImageFiles", "ListingId");
            AddForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings");
            DropForeignKey("dbo.Listings", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Listings", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Listings", new[] { "EmployeeId" });
            DropIndex("dbo.Listings", new[] { "CustomerId" });
            DropIndex("dbo.ImageFiles", new[] { "ListingId" });
            DropColumn("dbo.ImageFiles", "ListingId");
            DropTable("dbo.Listings");
        }
    }
}
