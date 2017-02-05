namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyIdNullableInContacts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_Contact", "CompanyId", "dbo.tbl_Company");
            DropIndex("dbo.tbl_Contact", new[] { "CompanyId" });
            AlterColumn("dbo.tbl_Contact", "CompanyId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_Contact", "CompanyId", c => c.Guid(nullable: false));
            CreateIndex("dbo.tbl_Contact", "CompanyId");
            AddForeignKey("dbo.tbl_Contact", "CompanyId", "dbo.tbl_Company", "Id");
        }
    }
}
