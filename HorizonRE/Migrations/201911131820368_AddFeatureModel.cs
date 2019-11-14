namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeatureModel : DbMigration
    {
        public override void Up()
        {

            Sql("Insert Into Features VALUES('Water view')");
            Sql("Insert Into Features VALUES('Close to schools')");
            Sql("Insert Into Features VALUES('Close to park')");
            Sql("Insert Into Features VALUES('On bus road')");
            Sql("Insert Into Features VALUES('Boilers heating')");
            Sql("Insert Into Features VALUES('Heat pumps')");
            Sql("Insert Into Features VALUES('Fireplace')");
            Sql("Insert Into Features VALUES('Electric space heater')");
            Sql("Insert Into Features VALUES('Garage')");
        }
        
        public override void Down()
        {
        }
    }
}
