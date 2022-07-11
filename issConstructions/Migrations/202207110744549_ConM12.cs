namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "CategoryId", "dbo.CategoryMasters");
            DropIndex("dbo.Issues", new[] { "CategoryId" });
            DropColumn("dbo.Issues", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "CategoryId");
            AddForeignKey("dbo.Issues", "CategoryId", "dbo.CategoryMasters", "ID", cascadeDelete: true);
        }
    }
}
