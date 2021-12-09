﻿namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequestTables", "Tax", c => c.String());
            DropColumn("dbo.PurchaseRequestTables", "productTax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequestTables", "productTax", c => c.Int(nullable: false));
            DropColumn("dbo.PurchaseRequestTables", "Tax");
        }
    }
}
