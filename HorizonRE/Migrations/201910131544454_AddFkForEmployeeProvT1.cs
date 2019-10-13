namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFkForEmployeeProvT1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmployeeProvinceId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmployeeProvinceId");
        }
    }
}
