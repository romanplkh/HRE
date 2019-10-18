namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeSeed : DbMigration
    {
        public override void Up()
        {
           //Sql("INSERT INTO Employees VALUES ('John', 'Doe', 'Frank', '123-123-123', '1234 Main street', 'Moncton', 'E1C1X1', '(506) 888-3342', '(506) 999-3365', '(506) 885-3321', 'johndoe@horizon.com', '1975-03-03 12:00:00', 1, '2019-01-01 12:00:00', 1)");
        }
        
        public override void Down()
        {
        }
    }
}
