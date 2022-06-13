namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IssuesDate = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        SiteAddressId = c.Int(nullable: false),
                        netAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        SiteDetails_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetails_ID)
                .Index(t => t.CategoryId)
                .Index(t => t.SiteDetails_ID);
            
            CreateTable(
                "dbo.IssueTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        issueId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Description = c.String(),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductNo = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
           
                .Index(t => t.productId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueTables", "productId", "dbo.ProductMasters");
            DropForeignKey("dbo.IssueTables", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.Issues", "SiteDetails_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.Issues", "CategoryId", "dbo.CategoryMasters");
            DropIndex("dbo.IssueTables", new[] { "CategoryId" });
            DropIndex("dbo.IssueTables", new[] { "productId" });
            DropIndex("dbo.Issues", new[] { "SiteDetails_ID" });
            DropIndex("dbo.Issues", new[] { "CategoryId" });
            DropTable("dbo.IssueTables");
            DropTable("dbo.Issues");
        }
    }
}
