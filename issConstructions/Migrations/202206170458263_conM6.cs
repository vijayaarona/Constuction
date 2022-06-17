namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conM6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Godowns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        godownName = c.String(),
                        address = c.String(),
                        authPerson = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Godowns");
        }
    }
}
