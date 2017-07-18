namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMinLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ActivityModels", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AssignmentModels", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ModulModels", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CourseModels", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseModels", "Name", c => c.String());
            AlterColumn("dbo.ModulModels", "Name", c => c.String());
            AlterColumn("dbo.AssignmentModels", "Name", c => c.String());
            AlterColumn("dbo.ActivityModels", "Name", c => c.String());
        }
    }
}
