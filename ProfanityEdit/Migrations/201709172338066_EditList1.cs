namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditList1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        MovieId = c.Int(nullable: false),
                        EditorId = c.String(maxLength: 128),
                        EditDate = c.DateTime(nullable: false),
                        GenerateMethodId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.EditorId)
                .ForeignKey("dbo.GenerateMethods", t => t.GenerateMethodId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.EditorId)
                .Index(t => t.GenerateMethodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditLists", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.EditLists", "GenerateMethodId", "dbo.GenerateMethods");
            DropForeignKey("dbo.EditLists", "EditorId", "dbo.AspNetUsers");
            DropIndex("dbo.EditLists", new[] { "GenerateMethodId" });
            DropIndex("dbo.EditLists", new[] { "EditorId" });
            DropIndex("dbo.EditLists", new[] { "MovieId" });
            DropTable("dbo.EditLists");
        }
    }
}
