namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecapFromProject : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //  "dbo.Countries",
            //  c => new
            //  {
            //      CountryId = c.Int(nullable: false, identity: true),
            //      Name = c.String(),
            //  })
            //  .PrimaryKey(t => t.CountryId);

            //CreateTable(
            //    "dbo.Employees",
            //    c => new
            //    {
            //        EmployeeId = c.Int(nullable: false, identity: true),
            //        FirstName = c.String(nullable: false, maxLength: 20),
            //        LastName = c.String(nullable: false, maxLength: 30),
            //        MiddleName = c.String(maxLength: 20),
            //        SIN = c.String(nullable: false, maxLength: 11),
            //        StreetAddress = c.String(nullable: false, maxLength: 100),
            //        City = c.String(nullable: false, maxLength: 50),
            //        PostalCode = c.String(nullable: false, maxLength: 6),
            //        HomePhone = c.String(nullable: false, maxLength: 14),
            //        CellPhone = c.String(nullable: false, maxLength: 14),
            //        OfficePhone = c.String(nullable: false, maxLength: 14),
            //        OfficeEmail = c.String(nullable: false, maxLength: 50),
            //        DOB = c.DateTime(nullable: false),
            //        AddedBy = c.Int(nullable: false),
            //        HireDate = c.DateTime(nullable: false),
            //        Country_CountryId = c.Int(),
            //        Province_ProvinceId = c.Int(),
            //    })
            //    .PrimaryKey(t => t.EmployeeId)
            //    .ForeignKey("dbo.Countries", t => t.Country_CountryId)
            //    .ForeignKey("dbo.Provinces", t => t.Province_ProvinceId)
            //    .Index(t => t.Country_CountryId)
            //    .Index(t => t.Province_ProvinceId);

            //CreateTable(
            //    "dbo.Provinces",
            //    c => new
            //    {
            //        ProvinceId = c.Int(nullable: false, identity: true),
            //        Name = c.String(),
            //    })
            //    .PrimaryKey(t => t.ProvinceId);


            Sql("INSERT INTO Countries VALUES('Canada')");
            Sql("INSERT INTO Countries VALUES('USA')");


            Sql("INSERT INTO Provinces VALUES('Alberta', 1)");
            Sql("INSERT INTO Provinces VALUES('British Columbia', 1)");
            Sql("INSERT INTO Provinces VALUES('Manitoba', 1)");
            Sql("INSERT INTO Provinces VALUES('New Brunswick', 1)");
            Sql("INSERT INTO Provinces VALUES('Newfoundland and Labrador', 1)");
            Sql("INSERT INTO Provinces VALUES('Northwest Territories', 1)");
            Sql("INSERT INTO Provinces VALUES('Nova Scotia', 1)");
            Sql("INSERT INTO Provinces VALUES('Nuvanut', 1)");
            Sql("INSERT INTO Provinces VALUES('Ontario', 1)");
            Sql("INSERT INTO Provinces VALUES('Prince Edward Island', 1)");
            Sql("INSERT INTO Provinces VALUES('Quebec', 1)");
            Sql("INSERT INTO Provinces VALUES('Saskatchewan', 1)");
            Sql("INSERT INTO Provinces VALUES('Yukon', 1)");
            Sql("INSERT INTO Provinces VALUES('Alabama', 2)");
            Sql("INSERT INTO Provinces VALUES('Alaska', 2)");
            Sql("INSERT INTO Provinces VALUES('Arizona', 2)");
            Sql("INSERT INTO Provinces VALUES('Arkansas', 2)");
            Sql("INSERT INTO Provinces VALUES('California', 2)");
            Sql("INSERT INTO Provinces VALUES('Colorado', 2)");
            Sql("INSERT INTO Provinces VALUES('Connecticut', 2)");
            Sql("INSERT INTO Provinces VALUES('Delaware', 2)");
            Sql("INSERT INTO Provinces VALUES('District of Columbia', 2)");
            Sql("INSERT INTO Provinces VALUES('Florida', 2)");
            Sql("INSERT INTO Provinces VALUES('Georgia', 2)");
            Sql("INSERT INTO Provinces VALUES('Hawaii', 2)");
            Sql("INSERT INTO Provinces VALUES('Idaho', 2)");
            Sql("INSERT INTO Provinces VALUES('Illinois', 2)");
            Sql("INSERT INTO Provinces VALUES('Indiana', 2)");
            Sql("INSERT INTO Provinces VALUES('Iowa', 2)");
            Sql("INSERT INTO Provinces VALUES('Kansas', 2)");
            Sql("INSERT INTO Provinces VALUES('Kentucky', 2)");
            Sql("INSERT INTO Provinces VALUES('Louisiana', 2)");
            Sql("INSERT INTO Provinces VALUES('Maine', 2)");
            Sql("INSERT INTO Provinces VALUES('Maryland', 2)");
            Sql("INSERT INTO Provinces VALUES('Massachusetts', 2)");
            Sql("INSERT INTO Provinces VALUES('Michigan', 2)");
            Sql("INSERT INTO Provinces VALUES('Minnesota', 2)");
            Sql("INSERT INTO Provinces VALUES('Mississippi', 2)");
            Sql("INSERT INTO Provinces VALUES('Missouri', 2)");
            Sql("INSERT INTO Provinces VALUES('Montana', 2)");
            Sql("INSERT INTO Provinces VALUES('Nebraska', 2)");
            Sql("INSERT INTO Provinces VALUES('Nevada', 2)");
            Sql("INSERT INTO Provinces VALUES('New Hampshire', 2)");
            Sql("INSERT INTO Provinces VALUES('New Jersey', 2)");
            Sql("INSERT INTO Provinces VALUES('New Mexico', 2)");
            Sql("INSERT INTO Provinces VALUES('New York', 2)");
            Sql("INSERT INTO Provinces VALUES('North Carolina', 2)");
            Sql("INSERT INTO Provinces VALUES('North Dakota', 2)");
            Sql("INSERT INTO Provinces VALUES('Ohio', 2)");
            Sql("INSERT INTO Provinces VALUES('Oklahoma', 2)");
            Sql("INSERT INTO Provinces VALUES('Oregon', 2)");
            Sql("INSERT INTO Provinces VALUES('Pennsylvania', 2)");
            Sql("INSERT INTO Provinces VALUES('Rhode Island', 2)");
            Sql("INSERT INTO Provinces VALUES('South Carolina', 2)");
            Sql("INSERT INTO Provinces VALUES('South Dakota', 2)");
            Sql("INSERT INTO Provinces VALUES('Tennessee', 2)");
            Sql("INSERT INTO Provinces VALUES('Texas', 2)");
            Sql("INSERT INTO Provinces VALUES('Utah', 2)");
            Sql("INSERT INTO Provinces VALUES('Vermont', 2)");
            Sql("INSERT INTO Provinces VALUES('Virginia', 2)");
            Sql("INSERT INTO Provinces VALUES('Washington', 2)");
            Sql("INSERT INTO Provinces VALUES('West Virginia', 2)");
            Sql("INSERT INTO Provinces VALUES('Wisconsin', 2)");
            Sql("INSERT INTO Provinces VALUES('Wyoming', 2)");


            //CreateTable(
            //    "dbo.Customers",
            //    c => new
            //    {
            //        CustomerId = c.Int(nullable: false, identity: true),
            //        FirstName = c.String(nullable: false, maxLength: 20),
            //        LastName = c.String(nullable: false, maxLength: 30),
            //        MiddleName = c.String(maxLength: 20),
            //        StreetAddress = c.String(nullable: false, maxLength: 100),
            //        City = c.String(nullable: false, maxLength: 50),
            //        PostalCode = c.String(nullable: false, maxLength: 6),
            //        Phone = c.String(nullable: false, maxLength: 14),
            //        Email = c.String(maxLength: 50),
            //        DOB = c.DateTime(nullable: false),
            //    })
            //    .PrimaryKey(t => t.CustomerId);

            //AddColumn("dbo.Provinces", "CountryId", c => c.Int(nullable: false));
            //CreateIndex("dbo.Provinces", "CountryId");
            //AddForeignKey("dbo.Provinces", "CountryId", "dbo.Countries", "CountryId");


           

            //CreateTable(
            //   "dbo.ProvinceCustomers",
            //   c => new
            //   {
            //       Id = c.Int(nullable: false, identity: true),
            //       ProvinceId = c.Int(nullable: false),
            //       CustomerId = c.Int(nullable: false),
            //   })
            //   .PrimaryKey(t => t.Id)
            //   .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
            //   .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
            //   .Index(t => t.ProvinceId)
            //   .Index(t => t.CustomerId);


            //AddColumn("dbo.Customers", "ProvinceCustomerId", c => c.Int(nullable: false));

            //AddColumn("dbo.Customers", "CustomerProvinceId", c => c.Int(nullable: false));
            //DropColumn("dbo.Customers", "ProvinceCustomerId");


            //AddColumn("dbo.Customers", "Country", c => c.String(nullable: false));
            //AddColumn("dbo.Customers", "Province", c => c.String(nullable: false));


            //DropForeignKey("dbo.Employees", "Country_CountryId", "dbo.Countries");
            //DropForeignKey("dbo.Employees", "Province_ProvinceId", "dbo.Provinces");
            //DropIndex("dbo.Employees", new[] { "Country_CountryId" });
            //DropIndex("dbo.Employees", new[] { "Province_ProvinceId" });
            //CreateTable(
            //    "dbo.ProvinceEmployees",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        ProvinceId = c.Int(nullable: false),
            //        EmployeeId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
            //    .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
            //    .Index(t => t.ProvinceId)
            //    .Index(t => t.EmployeeId);

            //DropColumn("dbo.Employees", "Country_CountryId");
            //DropColumn("dbo.Employees", "Province_ProvinceId");


            //AddColumn("dbo.Employees", "EmployeeProvinceId", c => c.Int(nullable: false));







        }

        public override void Down()
        {
            DropForeignKey("dbo.Employees", "Province_ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Employees", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Employees", new[] { "Province_ProvinceId" });
            DropIndex("dbo.Employees", new[] { "Country_CountryId" });
            DropTable("dbo.Provinces");
            DropTable("dbo.Employees");
            DropTable("dbo.Countries");


            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            DropColumn("dbo.Provinces", "CountryId");
            DropTable("dbo.Customers");
        }
    }
}
