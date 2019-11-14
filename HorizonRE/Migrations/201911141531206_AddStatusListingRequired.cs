namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusListingRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "Status", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "Status", c => c.String());
        }
    }
}
