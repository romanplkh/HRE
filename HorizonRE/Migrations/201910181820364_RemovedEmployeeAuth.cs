namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEmployeeAuth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Authentications", "AuthenticationId", "dbo.Employees");
            DropIndex("dbo.Authentications", new[] { "AuthenticationId" });
            DropPrimaryKey("dbo.Authentications");
            AddColumn("dbo.Employees", "Password", c => c.String());
            AlterColumn("dbo.Authentications", "AuthenticationId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Authentications", "AuthenticationId");
            DropColumn("dbo.Authentications", "AuthEmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authentications", "AuthEmployeeId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Authentications");
            AlterColumn("dbo.Authentications", "AuthenticationId", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "Password");
            AddPrimaryKey("dbo.Authentications", "AuthenticationId");
            CreateIndex("dbo.Authentications", "AuthenticationId");
            AddForeignKey("dbo.Authentications", "AuthenticationId", "dbo.Employees", "EmployeeId");
        }
    }
}
