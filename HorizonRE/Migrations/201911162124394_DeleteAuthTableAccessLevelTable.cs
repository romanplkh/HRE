namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAuthTableAccessLevelTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "AccessLevel_AccessLevelId", "dbo.AccessLevels");
            DropIndex("dbo.Employees", new[] { "AccessLevel_AccessLevelId" });
            DropColumn("dbo.Employees", "AccessLevel_AccessLevelId");
            DropTable("dbo.AccessLevels");
            DropTable("dbo.Authentications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Authentications",
                c => new
                    {
                        AuthenticationId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.AuthenticationId);
            
            CreateTable(
                "dbo.AccessLevels",
                c => new
                    {
                        AccessLevelId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.AccessLevelId);
            
            AddColumn("dbo.Employees", "AccessLevel_AccessLevelId", c => c.Int());
            CreateIndex("dbo.Employees", "AccessLevel_AccessLevelId");
            AddForeignKey("dbo.Employees", "AccessLevel_AccessLevelId", "dbo.AccessLevels", "AccessLevelId");
        }
    }
}
