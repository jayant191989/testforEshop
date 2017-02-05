namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productIsFeaturedNMore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Product", "IsAvailableForSale", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Product", "ShowOnWebsite", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Product", "Is_NewArrival", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Product", "Is_FeaturedProduct", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_Product", "Is_BestSeller", c => c.Boolean(nullable: false));
            DropColumn("dbo.tbl_StoreProduct", "IsAvailableForSale");
            DropColumn("dbo.tbl_StoreProduct", "ShowOnWebsite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_StoreProduct", "ShowOnWebsite", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_StoreProduct", "IsAvailableForSale", c => c.Boolean(nullable: false));
            DropColumn("dbo.tbl_Product", "Is_BestSeller");
            DropColumn("dbo.tbl_Product", "Is_FeaturedProduct");
            DropColumn("dbo.tbl_Product", "Is_NewArrival");
            DropColumn("dbo.tbl_Product", "ShowOnWebsite");
            DropColumn("dbo.tbl_Product", "IsAvailableForSale");
        }
    }
}
