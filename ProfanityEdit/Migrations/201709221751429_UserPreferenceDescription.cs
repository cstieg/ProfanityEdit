namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserPreferenceDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferenceSets", "Description", c => c.String(maxLength: 100));
            AddColumn("dbo.UserPreferenceSets", "Preset", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserPreferenceSets", "Preset");
            DropColumn("dbo.UserPreferenceSets", "Description");
        }
    }
}
