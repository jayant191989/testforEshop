namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdentityUserNAddress : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_UserAddress", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.tbl_UserAddress", new[] { "ApplicationUserId" });
            AddColumn("dbo.tbl_UserAddress", "ApplicationUserEmail", c => c.String());
            DropColumn("dbo.tbl_UserAddress", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_UserAddress", "ApplicationUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.tbl_UserAddress", "ApplicationUserEmail");
            CreateIndex("dbo.tbl_UserAddress", "ApplicationUserId");
            AddForeignKey("dbo.tbl_UserAddress", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
