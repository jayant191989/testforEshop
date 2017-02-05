namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullNameINProductIMage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_ProductImage", "FullName", c => c.String());
            AddColumn("dbo.tbl_ProductImage", "ImageName", c => c.String());
            DropColumn("dbo.tbl_ProductImage", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_ProductImage", "Name", c => c.String());
            DropColumn("dbo.tbl_ProductImage", "ImageName");
            DropColumn("dbo.tbl_ProductImage", "FullName");
        }
    }
}
