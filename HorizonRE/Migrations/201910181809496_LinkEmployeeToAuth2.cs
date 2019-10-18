namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkEmployeeToAuth2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authentications", "AuthEmployeeId", c => c.Int(nullable: false));
            DropColumn("dbo.Authentications", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authentications", "EmployeeId", c => c.Int(nullable: false));
            DropColumn("dbo.Authentications", "AuthEmployeeId");
        }
    }
}
