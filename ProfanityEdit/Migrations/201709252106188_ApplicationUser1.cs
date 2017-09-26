namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPreferenceItems", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.UserPreferenceItems", new[] { "UserPreferenceSetId" });
            AlterColumn("dbo.UserPreferenceItems", "UserPreferenceSetId", c => c.Int());
            CreateIndex("dbo.UserPreferenceItems", "UserPreferenceSetId");
            AddForeignKey("dbo.UserPreferenceItems", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPreferenceItems", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.UserPreferenceItems", new[] { "UserPreferenceSetId" });
            AlterColumn("dbo.UserPreferenceItems", "UserPreferenceSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserPreferenceItems", "UserPreferenceSetId");
            AddForeignKey("dbo.UserPreferenceItems", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id", cascadeDelete: true);
        }
    }
}
