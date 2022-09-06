namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM43 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtraWorkTables", "ExtraDate", c => c.DateTime());
            AddColumn("dbo.IssueTables", "IssueDate", c => c.DateTime());
            AddColumn("dbo.PurchaseEntryTables", "PurchaseDate", c => c.DateTime());
            AddColumn("dbo.PurchaseOrderTables", "OrderDate", c => c.DateTime());
            AddColumn("dbo.PurchaseRequestTables", "RequestDate", c => c.DateTime());
            AddColumn("dbo.RateWorkTables", "RateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RateWorkTables", "RateDate");
            DropColumn("dbo.PurchaseRequestTables", "RequestDate");
            DropColumn("dbo.PurchaseOrderTables", "OrderDate");
            DropColumn("dbo.PurchaseEntryTables", "PurchaseDate");
            DropColumn("dbo.IssueTables", "IssueDate");
            DropColumn("dbo.ExtraWorkTables", "ExtraDate");
        }
    }
}
