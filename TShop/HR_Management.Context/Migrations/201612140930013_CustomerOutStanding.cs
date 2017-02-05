namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerOutStanding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Contact", "OutStanding", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Contact", "OutStanding");
        }
    }
}
