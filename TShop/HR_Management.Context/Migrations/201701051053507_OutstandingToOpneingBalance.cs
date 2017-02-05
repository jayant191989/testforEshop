namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OutstandingToOpneingBalance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Contact", "OpeningBalance", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tbl_Contact", "OutStanding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Contact", "OutStanding", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tbl_Contact", "OpeningBalance");
        }
    }
}
