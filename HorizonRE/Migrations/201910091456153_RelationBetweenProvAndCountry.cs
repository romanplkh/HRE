namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationBetweenProvAndCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Provinces", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Provinces", "CountryId");
            AddForeignKey("dbo.Provinces", "CountryId", "dbo.Countries", "CountryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            DropColumn("dbo.Provinces", "CountryId");
        }
    }
}
