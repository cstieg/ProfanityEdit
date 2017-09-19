namespace ProfanityEdit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenerateMethodEnum : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EditLists", "GenerateMethodId", "dbo.GenerateMethods");
            DropIndex("dbo.EditLists", new[] { "GenerateMethodId" });
            AddColumn("dbo.EditLists", "GenerateMethod", c => c.Int(nullable: false));
            DropColumn("dbo.EditLists", "GenerateMethodId");
            DropTable("dbo.GenerateMethods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GenerateMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EditLists", "GenerateMethodId", c => c.Int(nullable: false));
            DropColumn("dbo.EditLists", "GenerateMethod");
            CreateIndex("dbo.EditLists", "GenerateMethodId");
            AddForeignKey("dbo.EditLists", "GenerateMethodId", "dbo.GenerateMethods", "Id", cascadeDelete: true);
        }
    }
}
