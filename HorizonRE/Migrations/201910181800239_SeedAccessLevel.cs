namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAccessLevel : DbMigration
    {
        public override void Up()
        {
          // Sql("INSERT INTO AccessLevels VALUES('Broker')");
          // Sql("INSERT INTO AccessLevels VALUES('Agent')");
           //Sql("INSERT INTO AccessLevels VALUES('Office Manager')");
        }
        
        public override void Down()
        {
        }
    }
}
