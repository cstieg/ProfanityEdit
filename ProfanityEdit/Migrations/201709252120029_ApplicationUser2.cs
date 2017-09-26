namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.AspNetUsers", new[] { "UserPreferenceSetId" });
            AlterColumn("dbo.AspNetUsers", "UserPreferenceSetId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "UserPreferenceSetId");
            AddForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.AspNetUsers", new[] { "UserPreferenceSetId" });
            AlterColumn("dbo.AspNetUsers", "UserPreferenceSetId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserPreferenceSetId");
            AddForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id", cascadeDelete: true);
        }
    }
}
