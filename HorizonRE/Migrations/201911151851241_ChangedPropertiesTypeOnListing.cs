namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPropertiesTypeOnListing : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "Bedrooms", c => c.Int(nullable: false));
            AlterColumn("dbo.Listings", "Bathrooms", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Bathrooms", c => c.Double(nullable: false));
            AlterColumn("dbo.Listings", "Bedrooms", c => c.String());
        }
    }
}
