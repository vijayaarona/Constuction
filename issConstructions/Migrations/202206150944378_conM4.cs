namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtraWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(),
                        projectId = c.Int(nullable: false),
                        siteNo = c.Int(nullable: false),
                        siteName = c.Int(nullable: false),
                        siteAddress = c.Int(nullable: false),
                        headName = c.String(),
                        totalAmt = c.Decimal(nullable: false, precision: 18, scale: 2),
                        passBy = c.String(),
                        remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        SiteDetails_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetails_ID)
                .Index(t => t.SiteDetails_ID);
            
            CreateTable(
                "dbo.ExtraWorkTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Breath = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deapth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nos = c.Int(nullable: false),
                        quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        unitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        umoId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        ProductMaster_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductMasters", t => t.ProductMaster_ID)
                .Index(t => t.ProductMaster_ID);
            
            CreateTable(
                "dbo.RateWorks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        projectId = c.Int(nullable: false),
                        siteNoId = c.Int(nullable: false),
                        siteNameId = c.Int(nullable: false),
                        siteAddressId = c.Int(nullable: false),
                        headName = c.String(),
                        totalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        deduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                        netAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        passedBy = c.String(),
                        remark = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        SiteDetails_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetails_ID)
                .Index(t => t.SiteDetails_ID);
            
            CreateTable(
                "dbo.RateWorkTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Breath = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Deapth = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Nos = c.Int(nullable: false),
                        quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        unitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        umoId = c.Int(nullable: false),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        ProductMaster_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductMasters", t => t.ProductMaster_ID)
                .Index(t => t.ProductMaster_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RateWorkTables", "ProductMaster_ID", "dbo.ProductMasters");
            DropForeignKey("dbo.RateWorks", "SiteDetails_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.ExtraWorkTables", "ProductMaster_ID", "dbo.ProductMasters");
            DropForeignKey("dbo.ExtraWorks", "SiteDetails_ID", "dbo.SiteDetails");
            DropIndex("dbo.RateWorkTables", new[] { "ProductMaster_ID" });
            DropIndex("dbo.RateWorks", new[] { "SiteDetails_ID" });
            DropIndex("dbo.ExtraWorkTables", new[] { "ProductMaster_ID" });
            DropIndex("dbo.ExtraWorks", new[] { "SiteDetails_ID" });
            DropTable("dbo.RateWorkTables");
            DropTable("dbo.RateWorks");
            DropTable("dbo.ExtraWorkTables");
            DropTable("dbo.ExtraWorks");
        }
    }
}
