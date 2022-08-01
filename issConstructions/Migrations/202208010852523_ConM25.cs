namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM25 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Issues", "SNameId");
            AddForeignKey("dbo.Issues", "SNameId", "dbo.SiteDetails", "ID", cascadeDelete: true );
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "SNameId", "dbo.SiteDetails");
            DropIndex("dbo.Issues", new[] { "SNameId" });
        }
    }
}
