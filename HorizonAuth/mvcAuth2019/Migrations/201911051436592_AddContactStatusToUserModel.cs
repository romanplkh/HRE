namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactStatusToUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "contactStatus", c => c.String(nullable: false, maxLength: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "contactStatus");
        }
    }
}
