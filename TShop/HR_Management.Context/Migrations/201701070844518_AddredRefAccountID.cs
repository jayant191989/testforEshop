namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddredRefAccountID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Daily", "RefAccountId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Daily", "RefAccountId");
        }
    }
}
