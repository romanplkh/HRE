namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFKToProvincesAddData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "Country_CountryId" });
            RenameColumn(table: "dbo.Provinces", name: "Country_CountryId", newName: "CountryId");
            AlterColumn("dbo.Provinces", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Provinces", "CountryId");
            AddForeignKey("dbo.Provinces", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            AlterColumn("dbo.Provinces", "CountryId", c => c.Int());
            RenameColumn(table: "dbo.Provinces", name: "CountryId", newName: "Country_CountryId");
            CreateIndex("dbo.Provinces", "Country_CountryId");
            AddForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
