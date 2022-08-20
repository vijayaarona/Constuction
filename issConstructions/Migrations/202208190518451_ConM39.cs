namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM39 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStocks", "PId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStocks", "PId");
        }
    }
}
