namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Userid", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserPreferenceSetId", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "UserPreferenceSetId");
            AddForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets");
            DropIndex("dbo.AspNetUsers", new[] { "UserPreferenceSetId" });
            DropColumn("dbo.AspNetUsers", "UserPreferenceSetId");
            DropColumn("dbo.AspNetUsers", "Userid");
        }
    }
}
