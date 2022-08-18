namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseEntries", "PType", c => c.String());
            DropColumn("dbo.PurchaseEntries", "PurType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseEntries", "PurType", c => c.String());
            DropColumn("dbo.PurchaseEntries", "PType");
        }
    }
}
