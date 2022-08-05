namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM34 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.paymentEntries", name: "siteDetailsId", newName: "siteNameId");
            RenameIndex(table: "dbo.paymentEntries", name: "IX_siteDetailsId", newName: "IX_siteNameId");
            AddColumn("dbo.paymentEntries", "LedgerId", c => c.Int(nullable: false));
            AddColumn("dbo.paymentEntries", "siteId", c => c.Int(nullable: false));
            CreateIndex("dbo.paymentEntries", "LedgerId");
            AddForeignKey("dbo.paymentEntries", "LedgerId", "dbo.AccountLedgerMasters", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.paymentEntries", "LedgerId", "dbo.AccountLedgerMasters");
            DropIndex("dbo.paymentEntries", new[] { "LedgerId" });
            DropColumn("dbo.paymentEntries", "siteId");
            DropColumn("dbo.paymentEntries", "LedgerId");
            RenameIndex(table: "dbo.paymentEntries", name: "IX_siteNameId", newName: "IX_siteDetailsId");
            RenameColumn(table: "dbo.paymentEntries", name: "siteNameId", newName: "siteDetailsId");
        }
    }
}
