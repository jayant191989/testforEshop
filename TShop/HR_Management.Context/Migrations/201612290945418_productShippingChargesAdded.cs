namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productShippingChargesAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_UserCart", "StoreProductId", "dbo.tbl_StoreProduct");
            DropIndex("dbo.tbl_UserCart", new[] { "StoreProductId" });
            AddColumn("dbo.tbl_Product", "StandardPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_Product", "StandardShippingCharges", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_Product", "StandardShippingTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_Product", "TwoDaysDeliveryCharges", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_Product", "TwoDaysDeliveryTime", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_UserCart", "ProductId", c => c.Guid(nullable: false));
            CreateIndex("dbo.tbl_UserCart", "ProductId");
            AddForeignKey("dbo.tbl_UserCart", "ProductId", "dbo.tbl_Product", "Id", cascadeDelete: true);
            DropColumn("dbo.tbl_UserCart", "StoreProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_UserCart", "StoreProductId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.tbl_UserCart", "ProductId", "dbo.tbl_Product");
            DropIndex("dbo.tbl_UserCart", new[] { "ProductId" });
            DropColumn("dbo.tbl_UserCart", "ProductId");
            DropColumn("dbo.tbl_Product", "TwoDaysDeliveryTime");
            DropColumn("dbo.tbl_Product", "TwoDaysDeliveryCharges");
            DropColumn("dbo.tbl_Product", "StandardShippingTime");
            DropColumn("dbo.tbl_Product", "StandardShippingCharges");
            DropColumn("dbo.tbl_Product", "StandardPrice");
            CreateIndex("dbo.tbl_UserCart", "StoreProductId");
            AddForeignKey("dbo.tbl_UserCart", "StoreProductId", "dbo.tbl_StoreProduct", "Id", cascadeDelete: true);
        }
    }
}
