namespace mvcAuth2019.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProfileDataToIdentityModel : DbMigration
    {
        public override void Up()
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
            CreateIndex("dbo.AspNetUsers", "UserProfileData_Id");
            AddForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfileDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserProfileData_Id", "dbo.UserProfileDatas");
            DropIndex("dbo.AspNetUsers", new[] { "UserProfileData_Id" });
            DropColumn("dbo.AspNetUsers", "UserProfileData_Id");
            DropTable("dbo.UserProfileDatas");
        }
    }
}
