namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPreference : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPreferenceItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        AllowLevel = c.Int(nullable: false),
                        UserPreferenceSet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.UserPreferenceSets", t => t.UserPreferenceSet_Id)
                .Index(t => t.CategoryId)
                .Index(t => t.UserPreferenceSet_Id);
            
            CreateTable(
                "dbo.UserPreferenceSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkipAudio = c.Boolean(nullable: false),
                        SkipVideo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPreferenceItems", "UserPreferenceSet_Id", "dbo.UserPreferenceSets");
            DropForeignKey("dbo.UserPreferenceItems", "CategoryId", "dbo.Categories");
            DropIndex("dbo.UserPreferenceItems", new[] { "UserPreferenceSet_Id" });
            DropIndex("dbo.UserPreferenceItems", new[] { "CategoryId" });
            DropTable("dbo.UserPreferenceSets");
            DropTable("dbo.UserPreferenceItems");
        }
    }
}
