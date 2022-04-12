namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PurchaseRequestTables");
            AlterColumn("dbo.PurchaseRequestTables", "ID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PurchaseRequestTables", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PurchaseRequestTables");
            AlterColumn("dbo.PurchaseRequestTables", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PurchaseRequestTables", "ID");
        }
    }
}
