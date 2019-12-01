namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        ListingId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId);
            
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
                        Email = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        Password = c.String(),
                        CustomerProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Listings",
                c => new
                    {
                        ListingId = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Province = c.String(),
                        Country = c.String(),
                        PostalCode = c.String(nullable: false),
                        Area = c.String(nullable: false),
                        Bedrooms = c.Int(nullable: false),
                        Bathrooms = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ContractSigned = c.Boolean(nullable: false),
                        ListingStartDate = c.DateTime(),
                        ListingEndDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        RenewNotificationSent = c.Boolean(nullable: false),
                        RenewDenialReason = c.String(),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(),
                        AreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ListingId)
                .ForeignKey("dbo.CityAreas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.CityAreas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AreaId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 30),
                        MiddleName = c.String(maxLength: 20),
                        SIN = c.String(nullable: false, maxLength: 11),
                        StreetAddress = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 50),
                        PostalCode = c.String(nullable: false, maxLength: 6),
                        HomePhone = c.String(nullable: false, maxLength: 14),
                        CellPhone = c.String(nullable: false, maxLength: 14),
                        OfficePhone = c.String(nullable: false, maxLength: 14),
                        OfficeEmail = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(nullable: false),
                        AddedBy = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        Password = c.String(),
                        EmployeeProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        ImageName = c.String(maxLength: 500),
                        ImageDescription = c.String(nullable: false, maxLength: 150),
                        Path = c.String(),
                        AltText = c.String(),
                        UploadDate = c.DateTime(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        ListingId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Listings", t => t.ListingId)
                .Index(t => t.EmployeeId)
                .Index(t => t.ListingId);
            
            CreateTable(
                "dbo.ProvinceEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProvinceId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProvinceCustomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProvinceId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.ProvinceId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProvinceId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.ListingFeature",
                c => new
                    {
                        ListingId = c.Int(nullable: false),
                        FeatureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ListingId, t.FeatureId })
                .ForeignKey("dbo.Listings", t => t.ListingId, cascadeDelete: true)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .Index(t => t.ListingId)
                .Index(t => t.FeatureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProvinceEmployees", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.ProvinceCustomers", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ProvinceCustomers", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ListingFeature", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.ListingFeature", "ListingId", "dbo.Listings");
            DropForeignKey("dbo.ProvinceEmployees", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Listings", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings");
            DropForeignKey("dbo.ImageFiles", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Listings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Listings", "AreaId", "dbo.CityAreas");
            DropForeignKey("dbo.Appointments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ListingFeature", new[] { "FeatureId" });
            DropIndex("dbo.ListingFeature", new[] { "ListingId" });
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            DropIndex("dbo.ProvinceCustomers", new[] { "CustomerId" });
            DropIndex("dbo.ProvinceCustomers", new[] { "ProvinceId" });
            DropIndex("dbo.ProvinceEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.ProvinceEmployees", new[] { "ProvinceId" });
            DropIndex("dbo.ImageFiles", new[] { "ListingId" });
            DropIndex("dbo.ImageFiles", new[] { "EmployeeId" });
            DropIndex("dbo.Listings", new[] { "AreaId" });
            DropIndex("dbo.Listings", new[] { "EmployeeId" });
            DropIndex("dbo.Listings", new[] { "CustomerId" });
            DropIndex("dbo.Appointments", new[] { "EmployeeId" });
            DropIndex("dbo.Appointments", new[] { "CustomerId" });
            DropTable("dbo.ListingFeature");
            DropTable("dbo.Provinces");
            DropTable("dbo.Countries");
            DropTable("dbo.ProvinceCustomers");
            DropTable("dbo.Features");
            DropTable("dbo.ProvinceEmployees");
            DropTable("dbo.ImageFiles");
            DropTable("dbo.Employees");
            DropTable("dbo.CityAreas");
            DropTable("dbo.Listings");
            DropTable("dbo.Customers");
            DropTable("dbo.Appointments");
        }
    }
}
