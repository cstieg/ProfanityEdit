namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BackToViewModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserPreferenceSets", "EditListId");
            DropColumn("dbo.UserPreferenceSets", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPreferenceSets", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserPreferenceSets", "EditListId", c => c.Int());
        }
    }
}
