namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2323 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AssignmentModels", "Feedback");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssignmentModels", "Feedback", c => c.String());
        }
    }
}
