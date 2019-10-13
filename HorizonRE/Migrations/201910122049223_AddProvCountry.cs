namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProvCountry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        MiddleName = c.String(maxLength: 20),
                        StreetAddress = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        PostalCode = c.String(nullable: false, maxLength: 6),
                        Phone = c.String(nullable: false, maxLength: 14),
                        Email = c.String(maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            AddColumn("dbo.Provinces", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Provinces", "CountryId");
            AddForeignKey("dbo.Provinces", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            DropColumn("dbo.Provinces", "CountryId");
            DropTable("dbo.Customers");
        }
    }
}
