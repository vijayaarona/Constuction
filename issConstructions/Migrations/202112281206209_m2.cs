namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropIndex("dbo.paymentEntries", new[] { "accountGroupId" });
            DropColumn("dbo.paymentEntries", "accountGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.paymentEntries", "accountGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.paymentEntries", "accountGroupId");
            AddForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters", "ID", cascadeDelete: true);
        }
    }
}
