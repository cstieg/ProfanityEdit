namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfanityAsk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Profanities", "Ask", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Profanities", "Ask");
        }
    }
}
