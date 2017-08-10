namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init10598 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ViewModuls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                        NrOfActivitys = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseModels", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            AddColumn("dbo.AssignmentModels", "Redo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ViewModuls", "CourseId", "dbo.CourseModels");
            DropIndex("dbo.ViewModuls", new[] { "CourseId" });
            DropColumn("dbo.AssignmentModels", "Redo");
            DropTable("dbo.ViewModuls");
        }
    }
}
