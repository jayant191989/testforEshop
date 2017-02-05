namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipIdInDaily : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MembershipParticulars", "Membership_Id", "dbo.tbl_Membership");
            DropForeignKey("dbo.MembershipParticulars", "Particular_Id", "dbo.tbl_Particular");
            DropIndex("dbo.MembershipParticulars", new[] { "Membership_Id" });
            DropIndex("dbo.MembershipParticulars", new[] { "Particular_Id" });
            AddColumn("dbo.tbl_Daily", "MembershipId", c => c.Guid());
            DropTable("dbo.MembershipParticulars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MembershipParticulars",
                c => new
                    {
                        Membership_Id = c.Guid(nullable: false),
                        Particular_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Membership_Id, t.Particular_Id });
            
            DropColumn("dbo.tbl_Daily", "MembershipId");
            CreateIndex("dbo.MembershipParticulars", "Particular_Id");
            CreateIndex("dbo.MembershipParticulars", "Membership_Id");
            AddForeignKey("dbo.MembershipParticulars", "Particular_Id", "dbo.tbl_Particular", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MembershipParticulars", "Membership_Id", "dbo.tbl_Membership", "Id", cascadeDelete: true);
        }
    }
}
