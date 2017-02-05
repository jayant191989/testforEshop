namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreditDebitInDaily : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Daily", "Credit", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_Daily", "Debit", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_Daily", "Net", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tbl_Daily", "AmountPaid");
            DropColumn("dbo.tbl_Daily", "AmountTaken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Daily", "AmountTaken", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_Daily", "AmountPaid", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tbl_Daily", "Net");
            DropColumn("dbo.tbl_Daily", "Debit");
            DropColumn("dbo.tbl_Daily", "Credit");
        }
    }
}
