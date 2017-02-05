namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCompnayRelationEnd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.tbl_Company");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyId" });
            DropColumn("dbo.AspNetUsers", "CompanyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "CompanyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CompanyId");
            AddForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.tbl_Company", "Id", cascadeDelete: true);
        }
    }
}
