namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SelectedPreferenceSet1 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserPreferenceSetId", "dbo.UserPreferenceSets");
        }
    }
}
