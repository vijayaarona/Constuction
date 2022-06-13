namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblStocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        categoryId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblStocks");
        }
    }
}
