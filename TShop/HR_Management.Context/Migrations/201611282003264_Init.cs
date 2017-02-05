namespace HR_Management.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_ApplicationForm",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CurrentDate = c.DateTime(nullable: false),
                        applicationFormStatus = c.Int(nullable: false),
                        Type = c.String(),
                        To = c.String(),
                        Subject = c.String(),
                        Description = c.String(),
                        FromDate = c.DateTime(),
                        TillDate = c.DateTime(),
                        EmployeeId = c.Int(),
                        CompanyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_Company",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyName = c.String(maxLength: 20),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true, name: "AK_Company_CompanyName");
            
            CreateTable(
                "dbo.tbl_Attendence",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.Date, unique: true, name: "AK_Attendence_Date")
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_EmployeeAttendence",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        InTime = c.String(),
                        OutTime = c.String(),
                        TotalTime = c.String(),
                        AttendenceType = c.String(),
                        WorkHours = c.Decimal(precision: 18, scale: 2),
                        OverTimeHours = c.Decimal(precision: 18, scale: 2),
                        EmployeeId = c.Guid(),
                        FullName = c.String(),
                        Status = c.Boolean(nullable: false),
                        AttendenceId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Attendence", t => t.AttendenceId, cascadeDelete: true)
                .Index(t => t.AttendenceId);
            
            CreateTable(
                "dbo.tbl_BankAccount",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BankName = c.String(),
                        IFSECode = c.String(),
                        AccountNumber = c.String(maxLength: 20),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        ContactId = c.Guid(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        Company_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.Company_Id)
                .ForeignKey("dbo.tbl_Contact", t => t.ContactId)
                .Index(t => t.AccountNumber, unique: true, name: "AK_BankAccount_AccountNumber")
                .Index(t => t.ContactId)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.tbl_Contact",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        HouseNumber = c.String(),
                        StreetName = c.String(),
                        LandMark = c.String(),
                        Colony = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Age = c.Int(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        FathersName = c.String(),
                        CustomerType = c.String(),
                        Anniversary = c.String(),
                        DateOfResignation = c.String(),
                        Email = c.String(nullable: false, maxLength: 50),
                        Mobile = c.String(),
                        EmpergencyContact = c.String(),
                        Acloholic = c.Boolean(),
                        Smoke = c.Boolean(),
                        MedicalHistory = c.String(),
                        Weight = c.String(),
                        Height = c.String(),
                        PersenalTrainer = c.Boolean(),
                        GymJoinedBefore = c.Boolean(),
                        DateOfJoining = c.DateTime(),
                        TrainingGoal = c.String(),
                        GymReference = c.String(),
                        MainImageName = c.String(),
                        ImageExtention = c.String(),
                        JoinDate = c.DateTime(),
                        BranchId = c.Guid(nullable: false),
                        DepartmentID = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        Status = c.Boolean(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Branch", t => t.BranchId)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId)
                .ForeignKey("dbo.tbl_Department", t => t.DepartmentID)
                .Index(t => t.Email, unique: true, name: "AK_Employee")
                .Index(t => t.BranchId)
                .Index(t => t.DepartmentID)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_Branch",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BranchCode = c.String(),
                        Name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CompanyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_Department",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        BranchId = c.Guid(nullable: false),
                        CompanyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Branch", t => t.BranchId)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId)
                .Index(t => t.Name, unique: true, name: "AK_Department")
                .Index(t => t.BranchId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_Batch",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        EnteredDate = c.DateTime(nullable: false),
                        StoreId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.tbl_Store",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_StoreProduct",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BatchNumber = c.String(),
                        ProductEnterDate = c.DateTime(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        CostPricePerUnit = c.Decimal(precision: 18, scale: 2),
                        DiscountRatePerUnit = c.Decimal(precision: 18, scale: 2),
                        MRPPerUnit = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        BatchId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Batch", t => t.BatchId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.tbl_DailyItems",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiscountRatePerUnit = c.Decimal(precision: 18, scale: 2),
                        MRPPerUnit = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        StoreProductId = c.Guid(nullable: false),
                        DailyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Daily", t => t.DailyId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_StoreProduct", t => t.StoreProductId)
                .Index(t => t.StoreProductId)
                .Index(t => t.DailyId);
            
            CreateTable(
                "dbo.tbl_Daily",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HeadName = c.String(nullable: false, maxLength: 80),
                        Invoice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        ContactId = c.Guid(nullable: false),
                        ParticularId = c.Guid(nullable: false),
                        Note = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Due = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Contact", t => t.ContactId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Particular", t => t.ParticularId, cascadeDelete: true)
                .Index(t => new { t.Invoice, t.HeadName }, unique: true, name: "AK_DailyInvoice")
                .Index(t => t.ContactId)
                .Index(t => t.ParticularId);
            
            CreateTable(
                "dbo.tbl_Particular",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Code = c.String(),
                        ModelNumber = c.String(),
                        AutoGenerateName = c.String(),
                        Name = c.String(),
                        Title = c.String(),
                        Discription = c.String(),
                        Specifications = c.String(),
                        ProductCategoryId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_ProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.tbl_ProductAttributeOptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ProductsAttributesId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_ProductsAttribute", t => t.ProductsAttributesId, cascadeDelete: true)
                .Index(t => t.ProductsAttributesId);
            
            CreateTable(
                "dbo.tbl_ProductsAttribute",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_ProductCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentCategoryId = c.Guid(),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_ProductCategory", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId)
                .Index(t => t.Name, unique: true, name: "AK_ProductCategory_CategoryName");
            
            CreateTable(
                "dbo.tbl_ProductImage",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Extention = c.String(),
                        ProductId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tbl_ProductVariant",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Discription = c.String(),
                        CostPrice = c.Decimal(precision: 18, scale: 2),
                        MRP = c.Decimal(precision: 18, scale: 2),
                        ProductId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tbl_CustomerFees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DueFees = c.Decimal(precision: 18, scale: 2),
                        SubmitFees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        EnrollCustomerId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_EnrollCustomer", t => t.EnrollCustomerId)
                .Index(t => t.EnrollCustomerId);
            
            CreateTable(
                "dbo.tbl_EnrollCustomer",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(),
                        FathersName = c.String(),
                        Email = c.String(nullable: false, maxLength: 80),
                        Mobile = c.String(),
                        DateOfJoining = c.DateTime(),
                        MembershipEndTime = c.DateTime(),
                        EnrolledDate = c.DateTime(),
                        DueTime = c.DateTime(),
                        MembershipId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Membership", t => t.MembershipId, cascadeDelete: true)
                .Index(t => t.MembershipId);
            
            CreateTable(
                "dbo.tbl_Membership",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MembershipName = c.String(),
                        SchemeStartDate = c.DateTime(),
                        SchemeEndDate = c.DateTime(),
                        TimePeriod = c.String(),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fees = c.Decimal(precision: 18, scale: 2),
                        CompanyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_EmployeeSalary",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        FullName = c.String(),
                        WorkHours = c.Decimal(precision: 18, scale: 2),
                        OverTimeHours = c.Decimal(precision: 18, scale: 2),
                        WorkHourTotal = c.Decimal(precision: 18, scale: 2),
                        RatePerHour = c.Decimal(precision: 18, scale: 2),
                        RatePerHourOvertime = c.Decimal(precision: 18, scale: 2),
                        OverTimeTotal = c.Decimal(precision: 18, scale: 2),
                        SalaryId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Salary", t => t.SalaryId, cascadeDelete: true)
                .Index(t => t.SalaryId);
            
            CreateTable(
                "dbo.tbl_Salary",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Date = c.DateTime(),
                        DaySalary = c.Decimal(precision: 18, scale: 2),
                        CompanyId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId)
                .Index(t => t.Date, unique: true, name: "AK_Salary_Date")
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.tbl_EmployeeSalaryDetail",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ESI = c.Boolean(),
                        PF = c.Boolean(),
                        Enrolled = c.Boolean(),
                        Status = c.String(),
                        RatePerHour = c.Decimal(precision: 18, scale: 2),
                        RatePerHourOvertime = c.Decimal(precision: 18, scale: 2),
                        FixedSalary = c.Decimal(precision: 18, scale: 2),
                        OverTimeCal = c.String(),
                        ContactId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Contact", t => t.ContactId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        LogEntryID = c.Long(nullable: false, identity: true),
                        LogDate = c.DateTime(nullable: false),
                        Logger = c.String(nullable: false, maxLength: 30),
                        LogLevel = c.String(nullable: false, maxLength: 5),
                        Thread = c.String(nullable: false, maxLength: 10),
                        EntityFormalNamePlural = c.String(nullable: false, maxLength: 30),
                        EntityKeyValue = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Message = c.String(nullable: false, maxLength: 256),
                        Exception = c.String(),
                    })
                .PrimaryKey(t => t.LogEntryID)
                .Index(t => new { t.EntityKeyValue, t.EntityFormalNamePlural }, name: "IDX_LogEntries_Entity");
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.tbl_SyncLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableId = c.Guid(nullable: false),
                        TableName = c.String(),
                        Action = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 15),
                        LastName = c.String(maxLength: 15),
                        Address = c.String(maxLength: 50),
                        City = c.String(maxLength: 20),
                        State = c.String(maxLength: 20),
                        ZipCode = c.String(maxLength: 10),
                        CompanyId = c.Guid(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProductAttributeOptionsProducts",
                c => new
                    {
                        ProductAttributeOptions_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductAttributeOptions_Id, t.Product_Id })
                .ForeignKey("dbo.tbl_ProductAttributeOptions", t => t.ProductAttributeOptions_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ProductAttributeOptions_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductCategoryProductsAttributes",
                c => new
                    {
                        ProductCategory_Id = c.Guid(nullable: false),
                        ProductsAttribute_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategory_Id, t.ProductsAttribute_Id })
                .ForeignKey("dbo.tbl_ProductCategory", t => t.ProductCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_ProductsAttribute", t => t.ProductsAttribute_Id, cascadeDelete: true)
                .Index(t => t.ProductCategory_Id)
                .Index(t => t.ProductsAttribute_Id);
            
            CreateTable(
                "dbo.ProductsAttributeProducts",
                c => new
                    {
                        ProductsAttribute_Id = c.Guid(nullable: false),
                        Product_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductsAttribute_Id, t.Product_Id })
                .ForeignKey("dbo.tbl_ProductsAttribute", t => t.ProductsAttribute_Id, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Product", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.ProductsAttribute_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tbl_EmployeeSalaryDetail", "ContactId", "dbo.tbl_Contact");
            DropForeignKey("dbo.tbl_EmployeeSalary", "SalaryId", "dbo.tbl_Salary");
            DropForeignKey("dbo.tbl_Salary", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_CustomerFees", "EnrollCustomerId", "dbo.tbl_EnrollCustomer");
            DropForeignKey("dbo.tbl_EnrollCustomer", "MembershipId", "dbo.tbl_Membership");
            DropForeignKey("dbo.tbl_Membership", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_StoreProduct", "ProductId", "dbo.tbl_Product");
            DropForeignKey("dbo.tbl_ProductVariant", "ProductId", "dbo.tbl_Product");
            DropForeignKey("dbo.tbl_ProductImage", "ProductId", "dbo.tbl_Product");
            DropForeignKey("dbo.ProductsAttributeProducts", "Product_Id", "dbo.tbl_Product");
            DropForeignKey("dbo.ProductsAttributeProducts", "ProductsAttribute_Id", "dbo.tbl_ProductsAttribute");
            DropForeignKey("dbo.ProductCategoryProductsAttributes", "ProductsAttribute_Id", "dbo.tbl_ProductsAttribute");
            DropForeignKey("dbo.ProductCategoryProductsAttributes", "ProductCategory_Id", "dbo.tbl_ProductCategory");
            DropForeignKey("dbo.tbl_Product", "ProductCategoryId", "dbo.tbl_ProductCategory");
            DropForeignKey("dbo.tbl_ProductCategory", "ParentCategoryId", "dbo.tbl_ProductCategory");
            DropForeignKey("dbo.tbl_ProductAttributeOptions", "ProductsAttributesId", "dbo.tbl_ProductsAttribute");
            DropForeignKey("dbo.ProductAttributeOptionsProducts", "Product_Id", "dbo.tbl_Product");
            DropForeignKey("dbo.ProductAttributeOptionsProducts", "ProductAttributeOptions_Id", "dbo.tbl_ProductAttributeOptions");
            DropForeignKey("dbo.tbl_DailyItems", "StoreProductId", "dbo.tbl_StoreProduct");
            DropForeignKey("dbo.tbl_Daily", "ParticularId", "dbo.tbl_Particular");
            DropForeignKey("dbo.tbl_DailyItems", "DailyId", "dbo.tbl_Daily");
            DropForeignKey("dbo.tbl_Daily", "ContactId", "dbo.tbl_Contact");
            DropForeignKey("dbo.tbl_StoreProduct", "BatchId", "dbo.tbl_Batch");
            DropForeignKey("dbo.tbl_Batch", "StoreId", "dbo.tbl_Store");
            DropForeignKey("dbo.tbl_BankAccount", "ContactId", "dbo.tbl_Contact");
            DropForeignKey("dbo.tbl_Contact", "DepartmentID", "dbo.tbl_Department");
            DropForeignKey("dbo.tbl_Contact", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_Contact", "BranchId", "dbo.tbl_Branch");
            DropForeignKey("dbo.tbl_Department", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_Department", "BranchId", "dbo.tbl_Branch");
            DropForeignKey("dbo.tbl_Branch", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_BankAccount", "Company_Id", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_EmployeeAttendence", "AttendenceId", "dbo.tbl_Attendence");
            DropForeignKey("dbo.tbl_Attendence", "CompanyId", "dbo.tbl_Company");
            DropForeignKey("dbo.tbl_ApplicationForm", "CompanyId", "dbo.tbl_Company");
            DropIndex("dbo.ProductsAttributeProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProductsAttributeProducts", new[] { "ProductsAttribute_Id" });
            DropIndex("dbo.ProductCategoryProductsAttributes", new[] { "ProductsAttribute_Id" });
            DropIndex("dbo.ProductCategoryProductsAttributes", new[] { "ProductCategory_Id" });
            DropIndex("dbo.ProductAttributeOptionsProducts", new[] { "Product_Id" });
            DropIndex("dbo.ProductAttributeOptionsProducts", new[] { "ProductAttributeOptions_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "CompanyId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LogEntries", "IDX_LogEntries_Entity");
            DropIndex("dbo.tbl_EmployeeSalaryDetail", new[] { "ContactId" });
            DropIndex("dbo.tbl_Salary", new[] { "CompanyId" });
            DropIndex("dbo.tbl_Salary", "AK_Salary_Date");
            DropIndex("dbo.tbl_EmployeeSalary", new[] { "SalaryId" });
            DropIndex("dbo.tbl_Membership", new[] { "CompanyId" });
            DropIndex("dbo.tbl_EnrollCustomer", new[] { "MembershipId" });
            DropIndex("dbo.tbl_CustomerFees", new[] { "EnrollCustomerId" });
            DropIndex("dbo.tbl_ProductVariant", new[] { "ProductId" });
            DropIndex("dbo.tbl_ProductImage", new[] { "ProductId" });
            DropIndex("dbo.tbl_ProductCategory", "AK_ProductCategory_CategoryName");
            DropIndex("dbo.tbl_ProductCategory", new[] { "ParentCategoryId" });
            DropIndex("dbo.tbl_ProductAttributeOptions", new[] { "ProductsAttributesId" });
            DropIndex("dbo.tbl_Product", new[] { "ProductCategoryId" });
            DropIndex("dbo.tbl_Daily", new[] { "ParticularId" });
            DropIndex("dbo.tbl_Daily", new[] { "ContactId" });
            DropIndex("dbo.tbl_Daily", "AK_DailyInvoice");
            DropIndex("dbo.tbl_DailyItems", new[] { "DailyId" });
            DropIndex("dbo.tbl_DailyItems", new[] { "StoreProductId" });
            DropIndex("dbo.tbl_StoreProduct", new[] { "BatchId" });
            DropIndex("dbo.tbl_StoreProduct", new[] { "ProductId" });
            DropIndex("dbo.tbl_Batch", new[] { "StoreId" });
            DropIndex("dbo.tbl_Department", new[] { "CompanyId" });
            DropIndex("dbo.tbl_Department", new[] { "BranchId" });
            DropIndex("dbo.tbl_Department", "AK_Department");
            DropIndex("dbo.tbl_Branch", new[] { "CompanyId" });
            DropIndex("dbo.tbl_Contact", new[] { "CompanyId" });
            DropIndex("dbo.tbl_Contact", new[] { "DepartmentID" });
            DropIndex("dbo.tbl_Contact", new[] { "BranchId" });
            DropIndex("dbo.tbl_Contact", "AK_Employee");
            DropIndex("dbo.tbl_BankAccount", new[] { "Company_Id" });
            DropIndex("dbo.tbl_BankAccount", new[] { "ContactId" });
            DropIndex("dbo.tbl_BankAccount", "AK_BankAccount_AccountNumber");
            DropIndex("dbo.tbl_EmployeeAttendence", new[] { "AttendenceId" });
            DropIndex("dbo.tbl_Attendence", new[] { "CompanyId" });
            DropIndex("dbo.tbl_Attendence", "AK_Attendence_Date");
            DropIndex("dbo.tbl_Company", "AK_Company_CompanyName");
            DropIndex("dbo.tbl_ApplicationForm", new[] { "CompanyId" });
            DropTable("dbo.ProductsAttributeProducts");
            DropTable("dbo.ProductCategoryProductsAttributes");
            DropTable("dbo.ProductAttributeOptionsProducts");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.tbl_SyncLog");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LogEntries");
            DropTable("dbo.tbl_EmployeeSalaryDetail");
            DropTable("dbo.tbl_Salary");
            DropTable("dbo.tbl_EmployeeSalary");
            DropTable("dbo.tbl_Membership");
            DropTable("dbo.tbl_EnrollCustomer");
            DropTable("dbo.tbl_CustomerFees");
            DropTable("dbo.tbl_ProductVariant");
            DropTable("dbo.tbl_ProductImage");
            DropTable("dbo.tbl_ProductCategory");
            DropTable("dbo.tbl_ProductsAttribute");
            DropTable("dbo.tbl_ProductAttributeOptions");
            DropTable("dbo.tbl_Product");
            DropTable("dbo.tbl_Particular");
            DropTable("dbo.tbl_Daily");
            DropTable("dbo.tbl_DailyItems");
            DropTable("dbo.tbl_StoreProduct");
            DropTable("dbo.tbl_Store");
            DropTable("dbo.tbl_Batch");
            DropTable("dbo.tbl_Department");
            DropTable("dbo.tbl_Branch");
            DropTable("dbo.tbl_Contact");
            DropTable("dbo.tbl_BankAccount");
            DropTable("dbo.tbl_EmployeeAttendence");
            DropTable("dbo.tbl_Attendence");
            DropTable("dbo.tbl_Company");
            DropTable("dbo.tbl_ApplicationForm");
        }
    }
}
