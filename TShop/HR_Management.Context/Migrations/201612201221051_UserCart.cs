namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_UserCart",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShippingCharges = c.Decimal(precision: 18, scale: 2),
                        DiscountPer = c.Decimal(precision: 18, scale: 2),
                        SessionId = c.String(),
                        UserId = c.String(),
                        StoreProductId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_StoreProduct", t => t.StoreProductId, cascadeDelete: true)
                .Index(t => t.StoreProductId);
            
            AlterColumn("dbo.tbl_StoreProduct", "SalePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_UserCart", "StoreProductId", "dbo.tbl_StoreProduct");
            DropIndex("dbo.tbl_UserCart", new[] { "StoreProductId" });
            AlterColumn("dbo.tbl_StoreProduct", "SalePrice", c => c.Decimal(precision: 18, scale: 2));
            DropTable("dbo.tbl_UserCart");
        }
    }
}
