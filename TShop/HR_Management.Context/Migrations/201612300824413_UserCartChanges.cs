namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCartChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbl_UserCart", "Price");
            DropColumn("dbo.tbl_UserCart", "ShippingCharges");
            DropColumn("dbo.tbl_UserCart", "DiscountPer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_UserCart", "DiscountPer", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_UserCart", "ShippingCharges", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_UserCart", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
