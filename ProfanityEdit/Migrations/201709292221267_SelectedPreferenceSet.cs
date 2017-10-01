namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SelectedPreferenceSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            AddColumn("dbo.AspNetUsers", "SelectedPreferenceSetId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "SelectedPreferenceSetId");
            AddForeignKey("dbo.AspNetUsers", "SelectedPreferenceSetId", "dbo.UserPreferenceSets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "SelectedPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.AspNetUsers", new[] { "SelectedPreferenceSetId" });
            DropColumn("dbo.AspNetUsers", "SelectedPreferenceSetId");
            AddForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id");
        }
    }
}
