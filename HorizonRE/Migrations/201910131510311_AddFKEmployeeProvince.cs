namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKEmployeeProvince : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Employees", "Province_ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Employees", new[] { "Country_CountryId" });
            DropIndex("dbo.Employees", new[] { "Province_ProvinceId" });
            CreateTable(
                "dbo.ProvinceEmployees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProvinceId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.ProvinceId)
                .Index(t => t.EmployeeId);
            
            DropColumn("dbo.Employees", "Country_CountryId");
            DropColumn("dbo.Employees", "Province_ProvinceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Province_ProvinceId", c => c.Int());
            AddColumn("dbo.Employees", "Country_CountryId", c => c.Int());
            DropForeignKey("dbo.ProvinceEmployees", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.ProvinceEmployees", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.ProvinceEmployees", new[] { "EmployeeId" });
            DropIndex("dbo.ProvinceEmployees", new[] { "ProvinceId" });
            DropTable("dbo.ProvinceEmployees");
            CreateIndex("dbo.Employees", "Province_ProvinceId");
            CreateIndex("dbo.Employees", "Country_CountryId");
            AddForeignKey("dbo.Employees", "Province_ProvinceId", "dbo.Provinces", "ProvinceId");
            AddForeignKey("dbo.Employees", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
