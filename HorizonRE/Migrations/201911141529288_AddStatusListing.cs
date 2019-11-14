namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusListing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "Status");
        }
    }
}
