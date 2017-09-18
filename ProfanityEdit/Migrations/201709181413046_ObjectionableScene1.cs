namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ObjectionableScene1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EditListItems", "ProfanityId", "dbo.Profanities");
            DropIndex("dbo.EditListItems", new[] { "ProfanityId" });
            AddColumn("dbo.EditListItems", "Description", c => c.String(maxLength: 100));
            AddColumn("dbo.EditListItems", "Audio", c => c.Boolean(nullable: false));
            AddColumn("dbo.EditListItems", "Video", c => c.Boolean(nullable: false));
            AddColumn("dbo.EditListItems", "ObjectionableSceneId", c => c.Int());
            AddColumn("dbo.ObjectionableScenes", "Description", c => c.String());
            AddColumn("dbo.ObjectionableScenes", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.ObjectionableScenes", "Level", c => c.Int(nullable: false));
            AlterColumn("dbo.EditListItems", "ProfanityId", c => c.Int());
            CreateIndex("dbo.EditListItems", "ProfanityId");
            CreateIndex("dbo.EditListItems", "ObjectionableSceneId");
            CreateIndex("dbo.ObjectionableScenes", "CategoryId");
            AddForeignKey("dbo.ObjectionableScenes", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EditListItems", "ObjectionableSceneId", "dbo.ObjectionableScenes", "Id");
            AddForeignKey("dbo.EditListItems", "ProfanityId", "dbo.Profanities", "Id");
            DropColumn("dbo.EditListItems", "Context");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EditListItems", "Context", c => c.String(nullable: false));
            DropForeignKey("dbo.EditListItems", "ProfanityId", "dbo.Profanities");
            DropForeignKey("dbo.EditListItems", "ObjectionableSceneId", "dbo.ObjectionableScenes");
            DropForeignKey("dbo.ObjectionableScenes", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ObjectionableScenes", new[] { "CategoryId" });
            DropIndex("dbo.EditListItems", new[] { "ObjectionableSceneId" });
            DropIndex("dbo.EditListItems", new[] { "ProfanityId" });
            AlterColumn("dbo.EditListItems", "ProfanityId", c => c.Int(nullable: false));
            DropColumn("dbo.ObjectionableScenes", "Level");
            DropColumn("dbo.ObjectionableScenes", "CategoryId");
            DropColumn("dbo.ObjectionableScenes", "Description");
            DropColumn("dbo.EditListItems", "ObjectionableSceneId");
            DropColumn("dbo.EditListItems", "Video");
            DropColumn("dbo.EditListItems", "Audio");
            DropColumn("dbo.EditListItems", "Description");
            CreateIndex("dbo.EditListItems", "ProfanityId");
            AddForeignKey("dbo.EditListItems", "ProfanityId", "dbo.Profanities", "Id", cascadeDelete: true);
        }
    }
}
