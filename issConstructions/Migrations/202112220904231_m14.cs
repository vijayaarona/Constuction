namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.masterTbls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        entryDate = c.DateTime(),
                        payType = c.String(),
                        accountLedgerName = c.String(),
                        accountGroup = c.String(),
                        description = c.String(),
                        expense = c.Decimal(nullable: false, precision: 18, scale: 2),
                        income = c.Decimal(nullable: false, precision: 18, scale: 2),
                        underGroup = c.String(),
                        type = c.String(),
                        financialYear = c.String(),
                        projectName = c.String(),
                        siteName = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.masterTbls");
        }
    }
}
