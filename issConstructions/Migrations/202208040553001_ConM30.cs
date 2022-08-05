namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM30 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExtraWorks", "SiteDetails_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.ExtraWorkTables", "ProductMaster_ID", "dbo.ProductMasters");
            DropIndex("dbo.ExtraWorks", new[] { "SiteDetails_ID" });
            DropIndex("dbo.ExtraWorkTables", new[] { "ProductMaster_ID" });
            DropColumn("dbo.ExtraWorkTables", "umoId");
            RenameColumn(table: "dbo.ExtraWorks", name: "SiteDetails_ID", newName: "SiteNameId");
            RenameColumn(table: "dbo.ExtraWorkTables", name: "ProductMaster_ID", newName: "umoId");
            AddColumn("dbo.ExtraWorks", "ExtraWorkId", c => c.Int(nullable: false));
            AddColumn("dbo.ExtraWorks", "siteNoId", c => c.Int(nullable: false));
            AddColumn("dbo.ExtraWorks", "siteId", c => c.Int(nullable: false));
            AddColumn("dbo.ExtraWorks", "siteAddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExtraWorks", "SiteNameId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExtraWorkTables", "umoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExtraWorks", "SiteNameId");
            CreateIndex("dbo.ExtraWorkTables", "umoId");
            AddForeignKey("dbo.ExtraWorks", "SiteNameId", "dbo.SiteDetails", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ExtraWorkTables", "umoId", "dbo.ProductMasters", "ID", cascadeDelete: true);
            DropColumn("dbo.ExtraWorks", "projectId");
            DropColumn("dbo.ExtraWorks", "siteNo");
            DropColumn("dbo.ExtraWorks", "siteName");
            DropColumn("dbo.ExtraWorks", "siteAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExtraWorks", "siteAddress", c => c.Int(nullable: false));
            AddColumn("dbo.ExtraWorks", "siteName", c => c.Int(nullable: false));
            AddColumn("dbo.ExtraWorks", "siteNo", c => c.Int(nullable: false));
            AddColumn("dbo.ExtraWorks", "projectId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ExtraWorkTables", "umoId", "dbo.ProductMasters");
            DropForeignKey("dbo.ExtraWorks", "SiteNameId", "dbo.SiteDetails");
            DropIndex("dbo.ExtraWorkTables", new[] { "umoId" });
            DropIndex("dbo.ExtraWorks", new[] { "SiteNameId" });
            AlterColumn("dbo.ExtraWorkTables", "umoId", c => c.Int());
            AlterColumn("dbo.ExtraWorks", "SiteNameId", c => c.Int());
            DropColumn("dbo.ExtraWorks", "siteAddressId");
            DropColumn("dbo.ExtraWorks", "siteId");
            DropColumn("dbo.ExtraWorks", "siteNoId");
            DropColumn("dbo.ExtraWorks", "ExtraWorkId");
            RenameColumn(table: "dbo.ExtraWorkTables", name: "umoId", newName: "ProductMaster_ID");
            RenameColumn(table: "dbo.ExtraWorks", name: "SiteNameId", newName: "SiteDetails_ID");
            AddColumn("dbo.ExtraWorkTables", "umoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExtraWorkTables", "ProductMaster_ID");
            CreateIndex("dbo.ExtraWorks", "SiteDetails_ID");
            AddForeignKey("dbo.ExtraWorkTables", "ProductMaster_ID", "dbo.ProductMasters", "ID");
            AddForeignKey("dbo.ExtraWorks", "SiteDetails_ID", "dbo.SiteDetails", "ID");
        }
    }
}
