namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SalePriceIsAvalableforSaleNShowOnWebSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_StoreProduct", "SalePrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_StoreProduct", "IsAvailableForSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_StoreProduct", "ShowOnWebsite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_StoreProduct", "ShowOnWebsite");
            DropColumn("dbo.tbl_StoreProduct", "IsAvailableForSale");
            DropColumn("dbo.tbl_StoreProduct", "SalePrice");
        }
    }
}
