namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblStocks", "IssueQty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tblStocks", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblStocks", "Type");
            DropColumn("dbo.tblStocks", "IssueQty");
        }
    }
}
