namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveKeyExplicit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageFiles", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.ImageFiles", new[] { "Employee_EmployeeId" });
            RenameColumn(table: "dbo.ImageFiles", name: "Employee_EmployeeId", newName: "EmployeeId");
            AlterColumn("dbo.ImageFiles", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ImageFiles", "EmployeeId");
            AddForeignKey("dbo.ImageFiles", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
            DropColumn("dbo.ImageFiles", "EmpId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageFiles", "EmpId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ImageFiles", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.ImageFiles", new[] { "EmployeeId" });
            AlterColumn("dbo.ImageFiles", "EmployeeId", c => c.Int());
            RenameColumn(table: "dbo.ImageFiles", name: "EmployeeId", newName: "Employee_EmployeeId");
            CreateIndex("dbo.ImageFiles", "Employee_EmployeeId");
            AddForeignKey("dbo.ImageFiles", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
