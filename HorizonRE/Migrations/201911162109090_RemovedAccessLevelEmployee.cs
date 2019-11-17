namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedAccessLevelEmployee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "AccessLevelId", "dbo.AccessLevels");
            DropIndex("dbo.Employees", new[] { "AccessLevelId" });
            RenameColumn(table: "dbo.Employees", name: "AccessLevelId", newName: "AccessLevel_AccessLevelId");
            AlterColumn("dbo.Employees", "AccessLevel_AccessLevelId", c => c.Int());
            CreateIndex("dbo.Employees", "AccessLevel_AccessLevelId");
            AddForeignKey("dbo.Employees", "AccessLevel_AccessLevelId", "dbo.AccessLevels", "AccessLevelId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "AccessLevel_AccessLevelId", "dbo.AccessLevels");
            DropIndex("dbo.Employees", new[] { "AccessLevel_AccessLevelId" });
            AlterColumn("dbo.Employees", "AccessLevel_AccessLevelId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Employees", name: "AccessLevel_AccessLevelId", newName: "AccessLevelId");
            CreateIndex("dbo.Employees", "AccessLevelId");
            AddForeignKey("dbo.Employees", "AccessLevelId", "dbo.AccessLevels", "AccessLevelId", cascadeDelete: true);
        }
    }
}
