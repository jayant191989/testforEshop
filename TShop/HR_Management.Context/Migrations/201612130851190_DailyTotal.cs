namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyTotal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Daily", "DailyTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Daily", "DailyTotal");
        }
    }
}
