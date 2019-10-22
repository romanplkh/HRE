namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableListingIdImages : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings");
            DropIndex("dbo.ImageFiles", new[] { "ListingId" });
            AlterColumn("dbo.ImageFiles", "ListingId", c => c.Int());
            CreateIndex("dbo.ImageFiles", "ListingId");
            AddForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings", "ListingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings");
            DropIndex("dbo.ImageFiles", new[] { "ListingId" });
            AlterColumn("dbo.ImageFiles", "ListingId", c => c.Int(nullable: false));
            CreateIndex("dbo.ImageFiles", "ListingId");
            AddForeignKey("dbo.ImageFiles", "ListingId", "dbo.Listings", "ListingId", cascadeDelete: true);
        }
    }
}
