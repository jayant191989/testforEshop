namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productImageNProductRelationChanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImageProducts", "ProductImage_Id", "dbo.tbl_ProductImage");
            DropForeignKey("dbo.ProductImageProducts", "Product_Id", "dbo.tbl_Product");
            DropIndex("dbo.ProductImageProducts", new[] { "ProductImage_Id" });
            DropIndex("dbo.ProductImageProducts", new[] { "Product_Id" });
            AddColumn("dbo.tbl_ProductImage", "ProductId", c => c.Guid(nullable: false));
            AlterColumn("dbo.tbl_ProductImage", "Size", c => c.Int(nullable: false));
            CreateIndex("dbo.tbl_ProductImage", "ProductId");
            AddForeignKey("dbo.tbl_ProductImage", "ProductId", "dbo.tbl_Product", "Id", cascadeDelete: true);
            DropTable("dbo.ProductImageProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductImageProducts",
                c => new
                    {
                        ProductImage_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductImage_Id, t.Product_Id });
            
            DropForeignKey("dbo.tbl_ProductImage", "ProductId", "dbo.tbl_Product");
            DropIndex("dbo.tbl_ProductImage", new[] { "ProductId" });
            AlterColumn("dbo.tbl_ProductImage", "Size", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.tbl_ProductImage", "ProductId");
            CreateIndex("dbo.ProductImageProducts", "Product_Id");
            CreateIndex("dbo.ProductImageProducts", "ProductImage_Id");
            AddForeignKey("dbo.ProductImageProducts", "Product_Id", "dbo.tbl_Product", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductImageProducts", "ProductImage_Id", "dbo.tbl_ProductImage", "Id", cascadeDelete: true);
        }
    }
}
