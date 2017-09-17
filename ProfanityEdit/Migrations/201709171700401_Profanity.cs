namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profanity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profanities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profanities", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Profanities", new[] { "CategoryId" });
            DropTable("dbo.Profanities");
            DropTable("dbo.Categories");
        }
    }
}
