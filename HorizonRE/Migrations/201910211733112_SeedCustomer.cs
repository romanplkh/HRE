namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedCustomer : DbMigration
    {
        public override void Up()
        {
            //Sql(
             //   "INSERT INTO CUSTOMERS VALUES ('Sam', 'Smith', 'Frank', '1234 Queen road', 'Winnipeg', 'E1CV31', '(531) 664-2212', 'samsmith@gmail.com', '1985-04-11', 3)");
        }
        
        public override void Down()
        {
        }
    }
}
