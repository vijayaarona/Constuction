namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "Sta", c => c.Int(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "Status", c => c.String());
            DropColumn("dbo.PurchaseRequests", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "Status", c => c.String());
            AlterColumn("dbo.PurchaseOrders", "Status", c => c.String());
            DropColumn("dbo.PurchaseRequests", "Sta");
        }
    }
}
