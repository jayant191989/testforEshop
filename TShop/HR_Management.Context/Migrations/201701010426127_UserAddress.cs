namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_UserAddress",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Company = c.String(),
                        AddressLine1 = c.String(maxLength: 50),
                        AddressLine2 = c.String(maxLength: 50),
                        City = c.String(maxLength: 30),
                        State = c.String(maxLength: 30),
                        Country = c.String(),
                        ZipCode = c.String(maxLength: 10),
                        Mobile = c.String(),
                        NearestLandMark = c.String(),
                        AddressTitle = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "State");
            DropColumn("dbo.AspNetUsers", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ZipCode", c => c.String(maxLength: 10));
            AddColumn("dbo.AspNetUsers", "State", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "Address", c => c.String(maxLength: 50));
            DropForeignKey("dbo.tbl_UserAddress", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.tbl_UserAddress", new[] { "ApplicationUserId" });
            DropTable("dbo.tbl_UserAddress");
        }
    }
}
