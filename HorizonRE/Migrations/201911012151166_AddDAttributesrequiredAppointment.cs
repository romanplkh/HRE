namespace HorizonRE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDAttributesrequiredAppointment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "Comment", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Appointments", "Comment", c => c.String());
        }
    }
}
