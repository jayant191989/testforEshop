namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalePriceNDiscountInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Product", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_Product", "DiscountPerUnitInPercent", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.tbl_StoreProduct", "SalePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_StoreProduct", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.tbl_Product", "DiscountPerUnitInPercent");
            DropColumn("dbo.tbl_Product", "SalePrice");
        }
    }
}
