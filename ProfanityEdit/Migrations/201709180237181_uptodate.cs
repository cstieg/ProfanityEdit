namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uptodate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditListItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EditListId = c.Int(nullable: false),
                        StartTime = c.Single(nullable: false),
                        EndTime = c.Single(nullable: false),
                        Context = c.String(nullable: false),
                        ProfanityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EditLists", t => t.EditListId, cascadeDelete: true)
                .ForeignKey("dbo.Profanities", t => t.ProfanityId, cascadeDelete: true)
                .Index(t => t.EditListId)
                .Index(t => t.ProfanityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditListItems", "ProfanityId", "dbo.Profanities");
            DropForeignKey("dbo.EditListItems", "EditListId", "dbo.EditLists");
            DropIndex("dbo.EditListItems", new[] { "ProfanityId" });
            DropIndex("dbo.EditListItems", new[] { "EditListId" });
            DropTable("dbo.EditListItems");
        }
    }
}
