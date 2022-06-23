namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToolsTransfers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TDate = c.DateTime(),
                        Type = c.String(),
                        FLocation = c.String(),
                        TLocation = c.String(),
                        ToolsId = c.Int(nullable: false),
                        qty = c.Int(nullable: false),
                        AuthPerson = c.String(),
                        ToolsName_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToolsMasters", t => t.ToolsName_ID)
                .Index(t => t.ToolsName_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToolsTransfers", "ToolsName_ID", "dbo.ToolsMasters");
            DropIndex("dbo.ToolsTransfers", new[] { "ToolsName_ID" });
            DropTable("dbo.ToolsTransfers");
        }
    }
}
