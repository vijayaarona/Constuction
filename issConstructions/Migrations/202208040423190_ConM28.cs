namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM28 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RateWorkTables", name: "ProductMaster_ID", newName: "Uom_ID");
            RenameIndex(table: "dbo.RateWorkTables", name: "IX_ProductMaster_ID", newName: "IX_Uom_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RateWorkTables", name: "IX_Uom_ID", newName: "IX_ProductMaster_ID");
            RenameColumn(table: "dbo.RateWorkTables", name: "Uom_ID", newName: "ProductMaster_ID");
        }
    }
}
