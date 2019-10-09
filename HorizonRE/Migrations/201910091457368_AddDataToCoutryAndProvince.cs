namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataToCoutryAndProvince : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Countries VALUES('Canada')");
            Sql("INSERT INTO Countries VALUES('USA')");

        }
        
        public override void Down()
        {
        }
    }
}
