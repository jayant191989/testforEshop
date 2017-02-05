namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParticularNMembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipParticulars",
                c => new
                    {
                        Membership_Id = c.Guid(nullable: false),
                        Particular_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Membership_Id, t.Particular_Id })
                .ForeignKey("dbo.tbl_Membership", t => t.Membership_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Particular", t => t.Particular_Id, cascadeDelete: true)
                .Index(t => t.Membership_Id)
                .Index(t => t.Particular_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MembershipParticulars", "Particular_Id", "dbo.tbl_Particular");
            DropForeignKey("dbo.MembershipParticulars", "Membership_Id", "dbo.tbl_Membership");
            DropIndex("dbo.MembershipParticulars", new[] { "Particular_Id" });
            DropIndex("dbo.MembershipParticulars", new[] { "Membership_Id" });
            DropTable("dbo.MembershipParticulars");
        }
    }
}
