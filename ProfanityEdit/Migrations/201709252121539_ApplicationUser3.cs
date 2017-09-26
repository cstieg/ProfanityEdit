namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationUser3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DisplayName", c => c.String(maxLength: 30));
            DropColumn("dbo.AspNetUsers", "Userid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Userid", c => c.String());
            DropColumn("dbo.AspNetUsers", "DisplayName");
        }
    }
}
