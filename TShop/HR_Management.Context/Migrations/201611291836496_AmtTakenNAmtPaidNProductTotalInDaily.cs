namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmtTakenNAmtPaidNProductTotalInDaily : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Daily", "ProductsTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_Daily", "AmountPaid", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_Daily", "AmountTaken", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tbl_Daily", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Daily", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.tbl_Daily", "AmountTaken");
            DropColumn("dbo.tbl_Daily", "AmountPaid");
            DropColumn("dbo.tbl_Daily", "ProductsTotal");
        }
    }
}
