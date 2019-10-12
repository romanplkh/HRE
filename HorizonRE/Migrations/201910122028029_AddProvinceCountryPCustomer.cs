namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProvinceCountryPCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Customers", "Province_ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Customers", new[] { "Country_CountryId" });
            DropIndex("dbo.Customers", new[] { "Province_ProvinceId" });
            CreateTable(
                "dbo.ProvinceCustomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProvinceId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.ProvinceId)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Provinces", "Country_CountryId", c => c.Int());
            CreateIndex("dbo.Provinces", "Country_CountryId");
            AddForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries", "CountryId");
            DropColumn("dbo.Customers", "Country_CountryId");
            DropColumn("dbo.Customers", "Province_ProvinceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Province_ProvinceId", c => c.Int());
            AddColumn("dbo.Customers", "Country_CountryId", c => c.Int());
            DropForeignKey("dbo.ProvinceCustomers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.ProvinceCustomers", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.ProvinceCustomers", new[] { "CustomerId" });
            DropIndex("dbo.ProvinceCustomers", new[] { "ProvinceId" });
            DropIndex("dbo.Provinces", new[] { "Country_CountryId" });
            DropColumn("dbo.Provinces", "Country_CountryId");
            DropTable("dbo.ProvinceCustomers");
            CreateIndex("dbo.Customers", "Province_ProvinceId");
            CreateIndex("dbo.Customers", "Country_CountryId");
            AddForeignKey("dbo.Customers", "Province_ProvinceId", "dbo.Provinces", "ProvinceId");
            AddForeignKey("dbo.Customers", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
