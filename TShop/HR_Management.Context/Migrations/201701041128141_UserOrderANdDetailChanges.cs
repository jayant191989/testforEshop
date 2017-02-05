namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOrderANdDetailChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_UserOrder", "Ip", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_UserOrder", "StateId", c => c.Guid());
            AddColumn("dbo.tbl_UserOrder", "StateName", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "ZipCode", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "StdCode", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "HomePhone", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "MobileNo", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "StateIdShipping", c => c.Guid());
            AddColumn("dbo.tbl_UserOrder", "ZipCodeShipping", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "StdCodeShipping", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "PhoneShipping", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "CouponCode", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "DiscountAmnt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.tbl_UserOrder", "IpAddress");
            DropColumn("dbo.tbl_UserOrder", "ShippingRate");
            DropColumn("dbo.tbl_UserOrder", "Username");
            DropColumn("dbo.tbl_UserOrder", "State");
            DropColumn("dbo.tbl_UserOrder", "PostalCode");
            DropColumn("dbo.tbl_UserOrder", "Phone");
            DropColumn("dbo.tbl_UserOrder", "Mobile");
            DropColumn("dbo.tbl_UserOrder", "PostalCodeShipping");
            DropColumn("dbo.tbl_UserOrder", "Total");
            DropColumn("dbo.tbl_UserOrder", "PaymentTransactionId");
            DropColumn("dbo.tbl_UserOrder", "HasBeenShipped");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_UserOrder", "HasBeenShipped", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_UserOrder", "PaymentTransactionId", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_UserOrder", "PostalCodeShipping", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "Mobile", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "Phone", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "PostalCode", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "State", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "Username", c => c.String());
            AddColumn("dbo.tbl_UserOrder", "ShippingRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.tbl_UserOrder", "IpAddress", c => c.String());
            DropColumn("dbo.tbl_UserOrder", "DiscountAmnt");
            DropColumn("dbo.tbl_UserOrder", "CouponCode");
            DropColumn("dbo.tbl_UserOrder", "PhoneShipping");
            DropColumn("dbo.tbl_UserOrder", "StdCodeShipping");
            DropColumn("dbo.tbl_UserOrder", "ZipCodeShipping");
            DropColumn("dbo.tbl_UserOrder", "StateIdShipping");
            DropColumn("dbo.tbl_UserOrder", "MobileNo");
            DropColumn("dbo.tbl_UserOrder", "HomePhone");
            DropColumn("dbo.tbl_UserOrder", "StdCode");
            DropColumn("dbo.tbl_UserOrder", "ZipCode");
            DropColumn("dbo.tbl_UserOrder", "StateName");
            DropColumn("dbo.tbl_UserOrder", "StateId");
            DropColumn("dbo.tbl_UserOrder", "TotalAmount");
            DropColumn("dbo.tbl_UserOrder", "Ip");
        }
    }
}
