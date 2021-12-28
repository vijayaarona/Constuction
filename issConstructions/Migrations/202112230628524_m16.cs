namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m16 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.paymentEntries", "accountGroup_ID", "dbo.AccountGroupMasters");
            DropForeignKey("dbo.receiptEntries", "accountGroup_ID", "dbo.AccountGroupMasters");
            DropIndex("dbo.paymentEntries", new[] { "accountGroup_ID" });
            DropIndex("dbo.receiptEntries", new[] { "accountGroup_ID" });
            RenameColumn(table: "dbo.paymentEntries", name: "accountGroup_ID", newName: "accountGroupId");
            RenameColumn(table: "dbo.receiptEntries", name: "accountGroup_ID", newName: "accountGroupId");
            AddColumn("dbo.paymentEntries", "parentNameId", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "parentNameId", c => c.Int(nullable: false));
            AlterColumn("dbo.paymentEntries", "accountGroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.receiptEntries", "accountGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.paymentEntries", "accountGroupId");
            CreateIndex("dbo.receiptEntries", "accountGroupId");
            AddForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters", "ID", cascadeDelete: true);
            AddForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropIndex("dbo.receiptEntries", new[] { "accountGroupId" });
            DropIndex("dbo.paymentEntries", new[] { "accountGroupId" });
            AlterColumn("dbo.receiptEntries", "accountGroupId", c => c.Int());
            AlterColumn("dbo.paymentEntries", "accountGroupId", c => c.Int());
            DropColumn("dbo.receiptEntries", "parentNameId");
            DropColumn("dbo.paymentEntries", "parentNameId");
            RenameColumn(table: "dbo.receiptEntries", name: "accountGroupId", newName: "accountGroup_ID");
            RenameColumn(table: "dbo.paymentEntries", name: "accountGroupId", newName: "accountGroup_ID");
            CreateIndex("dbo.receiptEntries", "accountGroup_ID");
            CreateIndex("dbo.paymentEntries", "accountGroup_ID");
            AddForeignKey("dbo.receiptEntries", "accountGroup_ID", "dbo.AccountGroupMasters", "ID");
            AddForeignKey("dbo.paymentEntries", "accountGroup_ID", "dbo.AccountGroupMasters", "ID");
        }
    }
}
