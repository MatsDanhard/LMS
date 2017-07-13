namespace LmsTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ViewStudents",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AssignmentModels", "ViewStudents_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AssignmentModels", "ViewStudents_Id");
            AddForeignKey("dbo.AssignmentModels", "ViewStudents_Id", "dbo.ViewStudents", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignmentModels", "ViewStudents_Id", "dbo.ViewStudents");
            DropIndex("dbo.AssignmentModels", new[] { "ViewStudents_Id" });
            DropColumn("dbo.AssignmentModels", "ViewStudents_Id");
            DropTable("dbo.ViewStudents");
        }
    }
}
