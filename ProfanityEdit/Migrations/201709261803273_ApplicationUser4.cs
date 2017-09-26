namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPreferenceSets", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserPreferenceSets", "EditList_Id", c => c.Int());
            CreateIndex("dbo.UserPreferenceSets", "EditList_Id");
            AddForeignKey("dbo.UserPreferenceSets", "EditList_Id", "dbo.EditLists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPreferenceSets", "EditList_Id", "dbo.EditLists");
            DropIndex("dbo.UserPreferenceSets", new[] { "EditList_Id" });
            DropColumn("dbo.UserPreferenceSets", "EditList_Id");
            DropColumn("dbo.UserPreferenceSets", "Discriminator");
        }
    }
}
