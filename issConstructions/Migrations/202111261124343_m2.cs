namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SupplierMasters", "SupplierId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SupplierMasters", "SupplierId");
        }
    }
}
