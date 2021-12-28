namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.masterTbls", "AccountID", c => c.String());
            AddColumn("dbo.masterTbls", "GroupID", c => c.String());
            DropColumn("dbo.masterTbls", "accountLedgerName");
            DropColumn("dbo.masterTbls", "accountGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.masterTbls", "accountGroup", c => c.String());
            AddColumn("dbo.masterTbls", "accountLedgerName", c => c.String());
            DropColumn("dbo.masterTbls", "GroupID");
            DropColumn("dbo.masterTbls", "AccountID");
        }
    }
}
