namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCityAreaClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityAreas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.AreaId);
            
            AddColumn("dbo.Listings", "AreaId", c => c.Int(nullable: false));
            CreateIndex("dbo.Listings", "AreaId");
            AddForeignKey("dbo.Listings", "AreaId", "dbo.CityAreas", "AreaId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "AreaId", "dbo.CityAreas");
            DropIndex("dbo.Listings", new[] { "AreaId" });
            DropColumn("dbo.Listings", "AreaId");
            DropTable("dbo.CityAreas");
        }
    }
}
