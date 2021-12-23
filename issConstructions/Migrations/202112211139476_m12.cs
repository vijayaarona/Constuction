namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.paymentEntries", "approvedBy", c => c.String());
            AddColumn("dbo.paymentEntries", "preparedBy", c => c.String());
            AddColumn("dbo.receiptEntries", "approvedBy", c => c.String());
            AddColumn("dbo.receiptEntries", "preparedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.receiptEntries", "preparedBy");
            DropColumn("dbo.receiptEntries", "approvedBy");
            DropColumn("dbo.paymentEntries", "preparedBy");
            DropColumn("dbo.paymentEntries", "approvedBy");
        }
    }
}
