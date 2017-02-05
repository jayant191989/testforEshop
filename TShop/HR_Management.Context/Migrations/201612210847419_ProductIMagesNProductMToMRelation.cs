namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductIMagesNProductMToMRelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_ProductImage", "ProductId", "dbo.tbl_Product");
            DropIndex("dbo.tbl_ProductImage", new[] { "ProductId" });
            CreateTable(
                "dbo.ProductImageProducts",
                c => new
                    {
                        ProductImage_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductImage_Id, t.Product_Id })
                .ForeignKey("dbo.tbl_ProductImage", t => t.ProductImage_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ProductImage_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.tbl_Product", "ImagePath", c => c.String());
            AddColumn("dbo.tbl_Product", "ImageSize", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.tbl_Product", "ImageExtention", c => c.String());
            DropColumn("dbo.tbl_Product", "ThumbImageName1");
            DropColumn("dbo.tbl_Product", "ThumbImageName2");
            DropColumn("dbo.tbl_Product", "ThumbImageName3");
            DropColumn("dbo.tbl_Product", "ThumbImageName4");
            DropColumn("dbo.tbl_ProductImage", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_ProductImage", "ProductId", c => c.Guid(nullable: false));
            AddColumn("dbo.tbl_Product", "ThumbImageName4", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName3", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName2", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName1", c => c.String());
            DropForeignKey("dbo.ProductImageProducts", "Product_Id", "dbo.tbl_Product");
            DropForeignKey("dbo.ProductImageProducts", "ProductImage_Id", "dbo.tbl_ProductImage");
            DropIndex("dbo.ProductImageProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProductImageProducts", new[] { "ProductImage_Id" });
            DropColumn("dbo.tbl_Product", "ImageExtention");
            DropColumn("dbo.tbl_Product", "ImageSize");
            DropColumn("dbo.tbl_Product", "ImagePath");
            DropTable("dbo.ProductImageProducts");
            CreateIndex("dbo.tbl_ProductImage", "ProductId");
            AddForeignKey("dbo.tbl_ProductImage", "ProductId", "dbo.tbl_Product", "Id", cascadeDelete: true);
        }
    }
}
