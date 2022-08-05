namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.receiptEntries", "LedgerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.receiptEntries", "LedgerId");
        }
    }
}
