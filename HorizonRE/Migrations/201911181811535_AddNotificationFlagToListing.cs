namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationFlagToListing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "RenewNotificationSent", c => c.Boolean(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "RenewNotificationSent");
        }
    }
}
