namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StartContractNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Listings", "ListingStartDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Listings", "ListingStartDate", c => c.DateTime(nullable: false));
        }
    }
}
