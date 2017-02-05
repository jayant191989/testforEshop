namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThumbImageInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Product", "ThumbMainImageName", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName1", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName2", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName3", c => c.String());
            AddColumn("dbo.tbl_Product", "ThumbImageName4", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Product", "ThumbImageName4");
            DropColumn("dbo.tbl_Product", "ThumbImageName3");
            DropColumn("dbo.tbl_Product", "ThumbImageName2");
            DropColumn("dbo.tbl_Product", "ThumbImageName1");
            DropColumn("dbo.tbl_Product", "ThumbMainImageName");
        }
    }
}
