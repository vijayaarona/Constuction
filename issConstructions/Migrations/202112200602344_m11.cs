namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.paymentEntries", "groupNameID", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "accountGroup_ID", c => c.Int());
            AddColumn("dbo.receiptEntries", "groupNameID", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "accountGroup_ID", c => c.Int());
            CreateIndex("dbo.paymentEntries", "accountGroup_ID");
            CreateIndex("dbo.receiptEntries", "accountGroup_ID");
            AddForeignKey("dbo.paymentEntries", "accountGroup_ID", "dbo.AccountGroupMasters", "ID");
            AddForeignKey("dbo.receiptEntries", "accountGroup_ID", "dbo.AccountGroupMasters", "ID");
            DropColumn("dbo.paymentEntries", "paymentTypeId");
            DropColumn("dbo.receiptEntries", "paymentTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.receiptEntries", "paymentTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "paymentTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.receiptEntries", "accountGroup_ID", "dbo.AccountGroupMasters");
            DropForeignKey("dbo.paymentEntries", "accountGroup_ID", "dbo.AccountGroupMasters");
            DropIndex("dbo.receiptEntries", new[] { "accountGroup_ID" });
            DropIndex("dbo.paymentEntries", new[] { "accountGroup_ID" });
            DropColumn("dbo.receiptEntries", "accountGroup_ID");
            DropColumn("dbo.receiptEntries", "groupNameID");
            DropColumn("dbo.paymentEntries", "accountGroup_ID");
            DropColumn("dbo.paymentEntries", "groupNameID");
        }
    }
}
