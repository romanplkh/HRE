namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullablePropertyEmployeeId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Listings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Listings", new[] { "EmployeeId" });
            AlterColumn("dbo.Listings", "EmployeeId", c => c.Int());
            CreateIndex("dbo.Listings", "EmployeeId");
            AddForeignKey("dbo.Listings", "EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Listings", new[] { "EmployeeId" });
            AlterColumn("dbo.Listings", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Listings", "EmployeeId");
            AddForeignKey("dbo.Listings", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
