namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM33 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.receiptEntries", "LedgerId");
            AddForeignKey("dbo.receiptEntries", "LedgerId", "dbo.AccountLedgerMasters", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.receiptEntries", "LedgerId", "dbo.AccountLedgerMasters");
            DropIndex("dbo.receiptEntries", new[] { "LedgerId" });
        }
    }
}
