namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRenewDenialReasonListingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Listings", "RenewDenialReason", c => c.String(nullable:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Listings", "RenewDenialReason");
        }
    }
}
