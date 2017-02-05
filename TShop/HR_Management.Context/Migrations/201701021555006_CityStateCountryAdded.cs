namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CityStateCountryAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_City",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        ZipCode = c.String(nullable: false),
                        StateId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.tbl_State",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false, maxLength: 10),
                        CountryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Country", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.tbl_Country",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Code = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "AK_Country_Name");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_City", "StateId", "dbo.tbl_State");
            DropForeignKey("dbo.tbl_State", "CountryId", "dbo.tbl_Country");
            DropIndex("dbo.tbl_Country", "AK_Country_Name");
            DropIndex("dbo.tbl_State", new[] { "CountryId" });
            DropIndex("dbo.tbl_City", new[] { "StateId" });
            DropTable("dbo.tbl_Country");
            DropTable("dbo.tbl_State");
            DropTable("dbo.tbl_City");
        }
    }
}
