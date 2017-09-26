namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeXspfViewModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPreferenceSets", "EditList_Id", "dbo.EditLists");
            DropIndex("dbo.UserPreferenceSets", new[] { "EditList_Id" });
            AddColumn("dbo.UserPreferenceSets", "EditListId", c => c.Int());
            DropColumn("dbo.UserPreferenceSets", "EditList_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPreferenceSets", "EditList_Id", c => c.Int());
            DropColumn("dbo.UserPreferenceSets", "EditListId");
            CreateIndex("dbo.UserPreferenceSets", "EditList_Id");
            AddForeignKey("dbo.UserPreferenceSets", "EditList_Id", "dbo.EditLists", "Id");
        }
    }
}
