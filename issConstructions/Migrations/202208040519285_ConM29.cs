namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConM29 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RateWorkTables", name: "UnitId", newName: "umoId");
            RenameIndex(table: "dbo.RateWorkTables", name: "IX_UnitId", newName: "IX_umoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RateWorkTables", name: "IX_umoId", newName: "IX_UnitId");
            RenameColumn(table: "dbo.RateWorkTables", name: "umoId", newName: "UnitId");
        }
    }
}
