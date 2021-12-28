namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.paymentEntries", "accountGroupNameId", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "accountGroupNameId", c => c.Int(nullable: false));
            DropColumn("dbo.paymentEntries", "parentNameId");
            DropColumn("dbo.receiptEntries", "parentNameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.receiptEntries", "parentNameId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "parentNameId", c => c.Int(nullable: false));
            DropColumn("dbo.receiptEntries", "accountGroupNameId");
            DropColumn("dbo.paymentEntries", "accountGroupNameId");
        }
    }
}
