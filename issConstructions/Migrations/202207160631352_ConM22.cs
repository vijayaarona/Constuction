namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "TypeId");
            AddForeignKey("dbo.Issues", "TypeId", "dbo.TblTypes", "Id", cascadeDelete: true);
            DropColumn("dbo.Issues", "IssueType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "IssueType", c => c.String());
            DropForeignKey("dbo.Issues", "TypeId", "dbo.TblTypes");
            DropIndex("dbo.Issues", new[] { "TypeId" });
            DropColumn("dbo.Issues", "TypeId");
        }
    }
}
