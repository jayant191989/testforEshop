namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeInContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Contact", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Contact", "Type");
        }
    }
}
