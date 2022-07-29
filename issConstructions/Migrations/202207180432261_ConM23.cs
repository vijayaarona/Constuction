namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "ProductNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "ProductNo");
        }
    }
}
