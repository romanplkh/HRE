namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIncreaseStatusLengthIdentityUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "contactStatus", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "contactStatus", c => c.String(nullable: false, maxLength: 7));
        }
    }
}
