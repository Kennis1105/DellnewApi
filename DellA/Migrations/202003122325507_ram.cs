namespace DellA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ram : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Specifications", "Ram", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Specifications", "Ram", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
