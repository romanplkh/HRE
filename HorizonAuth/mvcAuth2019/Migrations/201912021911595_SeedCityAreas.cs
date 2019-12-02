namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedCityAreas : DbMigration
    {
        public override void Up()
        {

            Sql("INSERT INTO CityAreas VALUES('North')");
            Sql("INSERT INTO CityAreas VALUES('South')");
            Sql("INSERT INTO CityAreas VALUES('East')");
            Sql("INSERT INTO CityAreas VALUES('West')");
            Sql("INSERT INTO CityAreas VALUES('North-East')");
            Sql("INSERT INTO CityAreas VALUES('North-West')");
            Sql("INSERT INTO CityAreas VALUES('South-East')");
            Sql("INSERT INTO CityAreas VALUES('South-West')");
        }
        
        public override void Down()
        {
        }
    }
}
