namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropIndex("dbo.paymentEntries", new[] { "accountGroupId" });
            DropIndex("dbo.receiptEntries", new[] { "accountGroupId" });
            DropColumn("dbo.paymentEntries", "accountGroupId");
            DropColumn("dbo.receiptEntries", "accountGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.receiptEntries", "accountGroupId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "accountGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.receiptEntries", "accountGroupId");
            CreateIndex("dbo.paymentEntries", "accountGroupId");
            AddForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters", "ID", cascadeDelete: true);
            AddForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters", "ID", cascadeDelete: true);
        }
    }
}
