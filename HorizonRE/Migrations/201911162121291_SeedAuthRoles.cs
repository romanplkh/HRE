namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAuthRoles : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AspNetRoles VALUES('Broker', 'Broker')");
            Sql("INSERT INTO AspNetRoles VALUES('Agent', 'Agent')");
            Sql("INSERT INTO AspNetRoles VALUES('Manager', 'Manager')");
            Sql("INSERT INTO AspNetRoles VALUES('Customer', 'Customer')");
        }
        
        public override void Down()
        {
        }
    }
}
