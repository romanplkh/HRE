namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationToCustomerTable : DbMigration
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
                        Country_CountryId = c.Int(),
                        Province_ProvinceId = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .ForeignKey("dbo.Provinces", t => t.Province_ProvinceId)
                .Index(t => t.Country_CountryId)
                .Index(t => t.Province_ProvinceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Province_ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Customers", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Customers", new[] { "Province_ProvinceId" });
            DropIndex("dbo.Customers", new[] { "Country_CountryId" });
            DropTable("dbo.Customers");
        }
    }
}
