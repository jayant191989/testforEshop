namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullDue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_Daily", "Due", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Daily", "Due", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
