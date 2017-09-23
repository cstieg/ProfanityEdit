namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPreferenceSetForeignKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPreferenceItems", "UserPreferenceSet_Id", "dbo.UserPreferenceSets");
            DropIndex("dbo.UserPreferenceItems", new[] { "UserPreferenceSet_Id" });
            RenameColumn(table: "dbo.UserPreferenceItems", name: "UserPreferenceSet_Id", newName: "UserPreferenceSetId");
            AlterColumn("dbo.UserPreferenceItems", "UserPreferenceSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserPreferenceItems", "UserPreferenceSetId");
            AddForeignKey("dbo.UserPreferenceItems", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPreferenceItems", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.UserPreferenceItems", new[] { "UserPreferenceSetId" });
            AlterColumn("dbo.UserPreferenceItems", "UserPreferenceSetId", c => c.Int());
            RenameColumn(table: "dbo.UserPreferenceItems", name: "UserPreferenceSetId", newName: "UserPreferenceSet_Id");
            CreateIndex("dbo.UserPreferenceItems", "UserPreferenceSet_Id");
            AddForeignKey("dbo.UserPreferenceItems", "UserPreferenceSet_Id", "dbo.UserPreferenceSets", "Id");
        }
    }
}
