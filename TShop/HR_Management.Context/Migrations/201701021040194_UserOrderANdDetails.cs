namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOrderANdDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_UserOrderDetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserOrderId = c.Guid(nullable: false),
                        Username = c.String(),
                        ProductId = c.Guid(nullable: false),
                        ProductTitle = c.String(),
                        ImagePath = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_UserOrder", t => t.UserOrderId, cascadeDelete: true)
                .Index(t => t.UserOrderId);
            
            CreateTable(
                "dbo.tbl_UserOrder",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.String(),
                        IpAddress = c.String(),
                        InvoiceNo = c.String(nullable: false, maxLength: 20),
                        OrderDate = c.DateTime(nullable: false),
                        ShippingRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        Country = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        FirstNameShipping = c.String(),
                        LastNameShipping = c.String(),
                        AddressShipping = c.String(),
                        CityShipping = c.String(),
                        StateShipping = c.String(),
                        PostalCodeShipping = c.String(),
                        CountryShipping = c.String(),
                        MobileShipping = c.String(),
                        IsPaid = c.Boolean(),
                        OrderStatus = c.String(),
                        TransactionId = c.String(),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentTransactionId = c.String(),
                        HasBeenShipped = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.InvoiceNo, unique: true, name: "UK_OrderInvoice");
            
            AddColumn("dbo.tbl_Product", "Is_FreeShipping", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_UserOrderDetail", "UserOrderId", "dbo.tbl_UserOrder");
            DropIndex("dbo.tbl_UserOrder", "UK_OrderInvoice");
            DropIndex("dbo.tbl_UserOrderDetail", new[] { "UserOrderId" });
            DropColumn("dbo.tbl_Product", "Is_FreeShipping");
            DropTable("dbo.tbl_UserOrder");
            DropTable("dbo.tbl_UserOrderDetail");
        }
    }
}
