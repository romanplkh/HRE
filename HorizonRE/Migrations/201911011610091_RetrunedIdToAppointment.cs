namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RetrunedIdToAppointment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "Employee_EmployeeId", "dbo.Employees");
            DropIndex("dbo.Appointments", new[] { "Employee_EmployeeId" });
            RenameColumn(table: "dbo.Appointments", name: "Employee_EmployeeId", newName: "EmployeeId");
            AlterColumn("dbo.Appointments", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Appointments", "EmployeeId");
            AddForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees", "EmployeeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Appointments", new[] { "EmployeeId" });
            AlterColumn("dbo.Appointments", "EmployeeId", c => c.Int());
            RenameColumn(table: "dbo.Appointments", name: "EmployeeId", newName: "Employee_EmployeeId");
            CreateIndex("dbo.Appointments", "Employee_EmployeeId");
            AddForeignKey("dbo.Appointments", "Employee_EmployeeId", "dbo.Employees", "EmployeeId");
        }
    }
}
