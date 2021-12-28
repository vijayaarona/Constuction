namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m18 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.paymentEntries", "AccountLedger_ID", "dbo.AccountLedgerMasters");
            DropForeignKey("dbo.receiptEntries", "AccountLedger_ID", "dbo.AccountLedgerMasters");
            DropForeignKey("dbo.paymentEntries", "SiteDetail_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.receiptEntries", "SiteDetail_ID", "dbo.SiteDetails");
            DropIndex("dbo.paymentEntries", new[] { "AccountLedger_ID" });
            DropIndex("dbo.paymentEntries", new[] { "SiteDetail_ID" });
            DropIndex("dbo.receiptEntries", new[] { "AccountLedger_ID" });
            DropIndex("dbo.receiptEntries", new[] { "SiteDetail_ID" });
            RenameColumn(table: "dbo.paymentEntries", name: "AccountLedger_ID", newName: "accountLedgerId");
            RenameColumn(table: "dbo.receiptEntries", name: "AccountLedger_ID", newName: "accountLedgerId");
            RenameColumn(table: "dbo.paymentEntries", name: "SiteDetail_ID", newName: "siteDetailsId");
            RenameColumn(table: "dbo.receiptEntries", name: "SiteDetail_ID", newName: "siteDetailsId");
            AlterColumn("dbo.paymentEntries", "accountLedgerId", c => c.Int(nullable: false));
            AlterColumn("dbo.paymentEntries", "siteDetailsId", c => c.Int(nullable: false));
            AlterColumn("dbo.receiptEntries", "accountLedgerId", c => c.Int(nullable: false));
            AlterColumn("dbo.receiptEntries", "siteDetailsId", c => c.Int(nullable: false));
            CreateIndex("dbo.paymentEntries", "accountLedgerId");
            CreateIndex("dbo.paymentEntries", "siteDetailsId");
            CreateIndex("dbo.receiptEntries", "accountLedgerId");
            CreateIndex("dbo.receiptEntries", "siteDetailsId");
            AddForeignKey("dbo.paymentEntries", "accountLedgerId", "dbo.AccountLedgerMasters", "ID", cascadeDelete: true);
            AddForeignKey("dbo.receiptEntries", "accountLedgerId", "dbo.AccountLedgerMasters", "ID", cascadeDelete: true);
            AddForeignKey("dbo.paymentEntries", "siteDetailsId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            AddForeignKey("dbo.receiptEntries", "siteDetailsId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            DropColumn("dbo.paymentEntries", "groupNameID");
            DropColumn("dbo.paymentEntries", "accountGroupNameId");
            DropColumn("dbo.paymentEntries", "accountLedgerNameId");
            DropColumn("dbo.paymentEntries", "projectNameId");
            DropColumn("dbo.paymentEntries", "siteNameId");
            DropColumn("dbo.receiptEntries", "groupNameID");
            DropColumn("dbo.receiptEntries", "accountGroupNameId");
            DropColumn("dbo.receiptEntries", "accountLedgerNameId");
            DropColumn("dbo.receiptEntries", "projectNameId");
            DropColumn("dbo.receiptEntries", "siteNameId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.receiptEntries", "siteNameId", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "projectNameId", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "accountLedgerNameId", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "accountGroupNameId", c => c.Int(nullable: false));
            AddColumn("dbo.receiptEntries", "groupNameID", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "siteNameId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "projectNameId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "accountLedgerNameId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "accountGroupNameId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "groupNameID", c => c.Int(nullable: false));
            DropForeignKey("dbo.receiptEntries", "siteDetailsId", "dbo.SiteDetails");
            DropForeignKey("dbo.paymentEntries", "siteDetailsId", "dbo.SiteDetails");
            DropForeignKey("dbo.receiptEntries", "accountLedgerId", "dbo.AccountLedgerMasters");
            DropForeignKey("dbo.paymentEntries", "accountLedgerId", "dbo.AccountLedgerMasters");
            DropIndex("dbo.receiptEntries", new[] { "siteDetailsId" });
            DropIndex("dbo.receiptEntries", new[] { "accountLedgerId" });
            DropIndex("dbo.paymentEntries", new[] { "siteDetailsId" });
            DropIndex("dbo.paymentEntries", new[] { "accountLedgerId" });
            AlterColumn("dbo.receiptEntries", "siteDetailsId", c => c.Int());
            AlterColumn("dbo.receiptEntries", "accountLedgerId", c => c.Int());
            AlterColumn("dbo.paymentEntries", "siteDetailsId", c => c.Int());
            AlterColumn("dbo.paymentEntries", "accountLedgerId", c => c.Int());
            RenameColumn(table: "dbo.receiptEntries", name: "siteDetailsId", newName: "SiteDetail_ID");
            RenameColumn(table: "dbo.paymentEntries", name: "siteDetailsId", newName: "SiteDetail_ID");
            RenameColumn(table: "dbo.receiptEntries", name: "accountLedgerId", newName: "AccountLedger_ID");
            RenameColumn(table: "dbo.paymentEntries", name: "accountLedgerId", newName: "AccountLedger_ID");
            CreateIndex("dbo.receiptEntries", "SiteDetail_ID");
            CreateIndex("dbo.receiptEntries", "AccountLedger_ID");
            CreateIndex("dbo.paymentEntries", "SiteDetail_ID");
            CreateIndex("dbo.paymentEntries", "AccountLedger_ID");
            AddForeignKey("dbo.receiptEntries", "SiteDetail_ID", "dbo.SiteDetails", "ID");
            AddForeignKey("dbo.paymentEntries", "SiteDetail_ID", "dbo.SiteDetails", "ID");
            AddForeignKey("dbo.receiptEntries", "AccountLedger_ID", "dbo.AccountLedgerMasters", "ID");
            AddForeignKey("dbo.paymentEntries", "AccountLedger_ID", "dbo.AccountLedgerMasters", "ID");
        }
    }
}
