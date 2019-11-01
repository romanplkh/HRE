namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedFkApp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Appointments", new[] { "EmployeeId" });
            RenameColumn(table: "dbo.Appointments", name: "EmployeeId", newName: "Employee_EmployeeId");
            AlterColumn("dbo.Appointments", "Employee_EmployeeId", c => c.Int());
            CreateIndex("dbo.Appointments", "Employee_EmployeeId");
            AddForeignKey("dbo.Appointments", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Appointments", new[] { "Employee_EmployeeId" });
            AlterColumn("dbo.Appointments", "Employee_EmployeeId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Appointments", name: "Employee_EmployeeId", newName: "EmployeeId");
            CreateIndex("dbo.Appointments", "EmployeeId");
            AddForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
    }
}
