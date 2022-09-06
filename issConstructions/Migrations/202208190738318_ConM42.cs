namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM42 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseOrders", "Sta", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseOrders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrders", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseOrders", "Sta");
        }
    }
}
