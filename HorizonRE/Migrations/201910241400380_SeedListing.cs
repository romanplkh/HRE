namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedListing : DbMigration
    {
        public override void Up()
        {
            //Sql("INSERT INTO Listings VALUES('24 West Street', 'Vancouver', 'British Columbia'," +
            //    "'Canada', 'R3V8K6', '500', '3', 3, 500000, 1, '2019-10-23', '2020-01-23', 1," +
            //    "1, 1)");
        }
        
        public override void Down()
        {
        }
    }
}
