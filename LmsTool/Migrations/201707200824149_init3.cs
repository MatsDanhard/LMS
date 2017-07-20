namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentModels", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.AssignmentModels", "Document", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentModels", "Document");
            DropColumn("dbo.AssignmentModels", "Approved");
        }
    }
}
