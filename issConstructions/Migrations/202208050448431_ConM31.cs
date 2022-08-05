namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM31 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.receiptEntries", name: "siteDetailsId", newName: "siteNameId");
            RenameIndex(table: "dbo.receiptEntries", name: "IX_siteDetailsId", newName: "IX_siteNameId");
            AddColumn("dbo.receiptEntries", "siteId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.receiptEntries", "siteId");
            RenameIndex(table: "dbo.receiptEntries", name: "IX_siteNameId", newName: "IX_siteDetailsId");
            RenameColumn(table: "dbo.receiptEntries", name: "siteNameId", newName: "siteDetailsId");
        }
    }
}
