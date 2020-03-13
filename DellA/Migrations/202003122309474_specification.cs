namespace DellA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class specification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        productID = c.Int(nullable: false),
                        Core = c.String(),
                        Windows = c.String(),
                        Graphics = c.String(),
                        Ram = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Drive = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Specifications");
        }
    }
}
