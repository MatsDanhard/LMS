namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssignmentModels", "StudentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssignmentModels", "StudentName");
        }
    }
}
