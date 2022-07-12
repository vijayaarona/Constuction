namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropIndex("dbo.receiptEntries", new[] { "accountGroupId" });
            DropColumn("dbo.receiptEntries", "accountGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.receiptEntries", "accountGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.receiptEntries", "accountGroupId");
            AddForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters", "ID", cascadeDelete: true);
        }
    }
}
