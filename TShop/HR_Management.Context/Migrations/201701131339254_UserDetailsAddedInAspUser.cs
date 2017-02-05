namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDetailsAddedInAspUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfileImage", c => c.String());
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "UpdatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "UpdatedBy", c => c.String(maxLength: 256));
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsActive");
            DropColumn("dbo.AspNetUsers", "UpdatedBy");
            DropColumn("dbo.AspNetUsers", "UpdatedDate");
            DropColumn("dbo.AspNetUsers", "CreatedBy");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
            DropColumn("dbo.AspNetUsers", "ProfileImage");
        }
    }
}
