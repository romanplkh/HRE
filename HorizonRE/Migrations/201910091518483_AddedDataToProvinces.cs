namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDataToProvinces : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Provinces VALUES('Alberta', 1)");
            Sql("INSERT INTO Provinces VALUES('British Columbia', 1)");
            Sql("INSERT INTO Provinces VALUES('Manitoba', 1)");
            Sql("INSERT INTO Provinces VALUES('New Brunswick', 1)");
            Sql("INSERT INTO Provinces VALUES('Newfoundland and Labrador', 1)");
            Sql("INSERT INTO Provinces VALUES('Northwest Territories', 1)");
            Sql("INSERT INTO Provinces VALUES('Nova Scotia', 1)");
            Sql("INSERT INTO Provinces VALUES('Nuvanut', 1)");
            Sql("INSERT INTO Provinces VALUES('Ontario', 1)");
            Sql("INSERT INTO Provinces VALUES('Prince Edward Island', 1)");
            Sql("INSERT INTO Provinces VALUES('Quebec', 1)");
            Sql("INSERT INTO Provinces VALUES('Saskatchewan', 1)");
            Sql("INSERT INTO Provinces VALUES('Yukon', 1)");
            Sql("INSERT INTO Provinces VALUES('Alabama', 2)");
            Sql("INSERT INTO Provinces VALUES('Alaska', 2)");
            Sql("INSERT INTO Provinces VALUES('Arizona', 2)");
            Sql("INSERT INTO Provinces VALUES('Arkansas', 2)");
            Sql("INSERT INTO Provinces VALUES('California', 2)");
            Sql("INSERT INTO Provinces VALUES('Colorado', 2)");
            Sql("INSERT INTO Provinces VALUES('Connecticut', 2)");
            Sql("INSERT INTO Provinces VALUES('Delaware', 2)");
            Sql("INSERT INTO Provinces VALUES('District of Columbia', 2)");
            Sql("INSERT INTO Provinces VALUES('Florida', 2)");
            Sql("INSERT INTO Provinces VALUES('Georgia', 2)");
            Sql("INSERT INTO Provinces VALUES('Hawaii', 2)");
            Sql("INSERT INTO Provinces VALUES('Idaho', 2)");
            Sql("INSERT INTO Provinces VALUES('Illinois', 2)");
            Sql("INSERT INTO Provinces VALUES('Indiana', 2)");
            Sql("INSERT INTO Provinces VALUES('Iowa', 2)");
            Sql("INSERT INTO Provinces VALUES('Kansas', 2)");
            Sql("INSERT INTO Provinces VALUES('Kentucky', 2)");
            Sql("INSERT INTO Provinces VALUES('Louisiana', 2)");
            Sql("INSERT INTO Provinces VALUES('Maine', 2)");
            Sql("INSERT INTO Provinces VALUES('Maryland', 2)");
            Sql("INSERT INTO Provinces VALUES('Massachusetts', 2)");
            Sql("INSERT INTO Provinces VALUES('Michigan', 2)");
            Sql("INSERT INTO Provinces VALUES('Minnesota', 2)");
            Sql("INSERT INTO Provinces VALUES('Mississippi', 2)");
            Sql("INSERT INTO Provinces VALUES('Missouri', 2)");
            Sql("INSERT INTO Provinces VALUES('Montana', 2)");
            Sql("INSERT INTO Provinces VALUES('Nebraska', 2)");
            Sql("INSERT INTO Provinces VALUES('Nevada', 2)");
            Sql("INSERT INTO Provinces VALUES('New Hampshire', 2)");
            Sql("INSERT INTO Provinces VALUES('New Jersey', 2)");
            Sql("INSERT INTO Provinces VALUES('New Mexico', 2)");
            Sql("INSERT INTO Provinces VALUES('New York', 2)");
            Sql("INSERT INTO Provinces VALUES('North Carolina', 2)");
            Sql("INSERT INTO Provinces VALUES('North Dakota', 2)");
            Sql("INSERT INTO Provinces VALUES('Ohio', 2)");
            Sql("INSERT INTO Provinces VALUES('Oklahoma', 2)");
            Sql("INSERT INTO Provinces VALUES('Oregon', 2)");
            Sql("INSERT INTO Provinces VALUES('Pennsylvania', 2)");
            Sql("INSERT INTO Provinces VALUES('Rhode Island', 2)");
            Sql("INSERT INTO Provinces VALUES('South Carolina', 2)");
            Sql("INSERT INTO Provinces VALUES('South Dakota', 2)");
            Sql("INSERT INTO Provinces VALUES('Tennessee', 2)");
            Sql("INSERT INTO Provinces VALUES('Texas', 2)");
            Sql("INSERT INTO Provinces VALUES('Utah', 2)");
            Sql("INSERT INTO Provinces VALUES('Vermont', 2)");
            Sql("INSERT INTO Provinces VALUES('Virginia', 2)");
            Sql("INSERT INTO Provinces VALUES('Washington', 2)");
            Sql("INSERT INTO Provinces VALUES('West Virginia', 2)");
            Sql("INSERT INTO Provinces VALUES('Wisconsin', 2)");
            Sql("INSERT INTO Provinces VALUES('Wyoming', 2)");

        }

        public override void Down()
        {
        }
    }
}
