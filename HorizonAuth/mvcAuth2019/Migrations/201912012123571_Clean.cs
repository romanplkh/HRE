namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clean : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfileDatas");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfileData_Id" });
            DropColumn("dbo.AspNetUsers", "contactStatus");
            DropColumn("dbo.AspNetUsers", "UserProfileData_Id");
            DropTable("dbo.UserProfileDatas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProfileDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NickName = c.String(),
                        ProfilePic = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "UserProfileData_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "contactStatus", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.AspNetUsers", "UserProfileData_Id");
            AddForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfileDatas", "Id");
        }
    }
}
