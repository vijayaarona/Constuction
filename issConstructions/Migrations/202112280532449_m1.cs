namespace issConstructions.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountGroupMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        ParentGroup = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccountLedgerMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountLedger = c.String(),
                        OpeningBal = c.Int(nullable: false),
                        AccountGroupID = c.Int(nullable: false),
                        Type = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountGroupMasters", t => t.AccountGroupID, cascadeDelete: true)
                .Index(t => t.AccountGroupID);
            
            CreateTable(
                "dbo.CategoryMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NameoftheCompany = c.String(),
                        PrintName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        EmailID = c.String(),
                        PhoneNumber = c.String(),
                        GSTNumber = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DesignationMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DesignationName = c.String(),
                        Remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmployeeMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        address = c.String(),
                        Emailid = c.String(),
                        Mobilenumber = c.String(),
                        Status = c.String(),
                        SalaryType = c.String(),
                        SalaryAmount = c.String(),
                        City = c.String(),
                        DesignationId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        DateofJoin = c.DateTime(),
                        DateofRelive = c.DateTime(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.DesignationMasters", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DesignationId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Errorlogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Errordate = c.DateTime(nullable: false),
                        controllername = c.String(),
                        MethodNmae = c.String(),
                        ErrorMessage = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.masterTbls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        entryDate = c.DateTime(),
                        payType = c.String(),
                        AccountID = c.String(),
                        GroupID = c.String(),
                        description = c.String(),
                        expense = c.Decimal(nullable: false, precision: 18, scale: 2),
                        income = c.Decimal(nullable: false, precision: 18, scale: 2),
                        underGroup = c.String(),
                        type = c.String(),
                        financialYear = c.String(),
                        projectName = c.String(),
                        siteName = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.paymentEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        paymentID = c.Int(nullable: false),
                        paymenttDate = c.DateTime(),
                        accountGroupId = c.Int(nullable: false),
                        accountLedgerId = c.Int(nullable: false),
                        siteDetailsId = c.Int(nullable: false),
                        givenBy = c.String(),
                        collectBy = c.String(),
                        approvedBy = c.String(),
                        preparedBy = c.String(),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                //.ForeignKey("dbo.AccountGroupMasters", t => t.accountGroupId, cascadeDelete: true)
                //.ForeignKey("dbo.AccountLedgerMasters", t => t.accountLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.SiteDetails", t => t.siteDetailsId, cascadeDelete: true)
                .Index(t => t.accountGroupId)
                .Index(t => t.accountLedgerId)
                .Index(t => t.siteDetailsId);
            
            CreateTable(
                "dbo.SiteDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectDate = c.DateTime(),
                        ProjectName = c.String(),
                        SiteName = c.String(),
                        SiteAddress = c.String(),
                        ClientName = c.String(),
                        ClientAddress = c.String(),
                        MobileNo1 = c.String(),
                        MobileNo2 = c.String(),
                        Email1 = c.String(),
                        Email2 = c.String(),
                        DateOfStart = c.DateTime(),
                        ReffBy = c.String(),
                        ReffContactNo = c.String(),
                        Branch = c.String(),
                        NatureOfWork = c.String(),
                        WorkStatus = c.String(),
                        ClientType = c.String(),
                        SupervisorName = c.String(),
                        Length = c.String(),
                        Breath = c.String(),
                        Sqrft = c.String(),
                        BuildupArea = c.String(),
                        FSI = c.String(),
                        NoOfFloor = c.String(),
                        CostPerSqrft = c.String(),
                        TotalCost = c.String(),
                        AdditionalCost = c.String(),
                        NetCost = c.String(),
                        NoOfColoumn = c.String(),
                        MeasurmentOfBelt = c.String(),
                        payment1 = c.String(),
                        payment2 = c.String(),
                        payment3 = c.String(),
                        payment4 = c.String(),
                        payment5 = c.String(),
                        payment1Description = c.String(),
                        payment2Description = c.String(),
                        payment3Description = c.String(),
                        payment4Description = c.String(),
                        payment5Description = c.String(),
                        TypeOfWork = c.String(),
                        WorkDescription = c.String(),
                        Field = c.String(),
                        Department = c.String(),
                        TenderName = c.String(),
                        TenderDate = c.DateTime(),
                        TenderEMDCost = c.String(),
                        WorkAllocated = c.String(),
                        AgreementTime = c.DateTime(),
                        SecurityDeposit = c.String(),
                        AdditionalSD = c.String(),
                        BusinessFund = c.String(),
                        Reduction = c.String(),
                        TenderDD = c.String(),
                        BankName = c.String(),
                        DDNo = c.String(),
                        DDDate = c.DateTime(),
                        RefundDate = c.DateTime(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        UOM = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PurchaseEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        Invoice = c.String(),
                        purchaseId = c.Int(nullable: false),
                        purchaseDate = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        SupplierAddressId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        SiteAddressId = c.Int(nullable: false),
                        mobileno = c.String(),
                        ReceivedBy = c.String(),
                        Remarks = c.String(),
                        ReffBillNo = c.String(),
                        DeliveryNo = c.String(),
                        totalDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        totalTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        freightCharges = c.Decimal(nullable: false, precision: 18, scale: 2),
                        netAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        PurchaseOrder_ID = c.Int(),
                        SiteDetails_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrders", t => t.PurchaseOrder_ID)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetails_ID)
                .ForeignKey("dbo.SupplierMasters", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.PurchaseOrder_ID)
                .Index(t => t.SiteDetails_ID);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestID = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        OrderDate = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        SupplierAddressId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        SiteAddressId = c.Int(nullable: false),
                        mobileno = c.String(),
                        netAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                        requestBy = c.String(),
                        orderby = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        PurchaseRequest_ID = c.Int(),
                        SiteDetails_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequest_ID)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetails_ID)
                .ForeignKey("dbo.SupplierMasters", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.PurchaseRequest_ID)
                .Index(t => t.SiteDetails_ID);
            
            CreateTable(
                "dbo.PurchaseRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RequestID = c.Int(nullable: false),
                        RequestDate = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        SupplierAddressId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        SiteId = c.Int(nullable: false),
                        SiteAddressId = c.Int(nullable: false),
                        mobileno = c.String(),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RequestBy = c.String(),
                        Remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        SiteDetails_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SiteDetails", t => t.SiteDetails_ID)
                .ForeignKey("dbo.SupplierMasters", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.SiteDetails_ID);
            
            CreateTable(
                "dbo.SupplierMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Suppliername = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        address = c.String(),
                        Contactperson = c.String(),
                        Phonenumber = c.String(),
                        EmailID = c.String(),
                        GSTnumber = c.String(),
                        MSMEnumber = c.String(),
                        OpeningBalance = c.Int(nullable: false),
                        Bankname = c.String(),
                        Accountnumber = c.String(),
                        Branch = c.String(),
                        IFSCcode = c.String(),
                        CategoryId = c.Int(nullable: false),
                        City = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                //.ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.PurchaseEntryTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        purchaseRequestId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        Description = c.String(),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductMasters", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.PurchaseOrderTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        purchaseRequestId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        Description = c.String(),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductMasters", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.PurchaseRequestTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        purchaseRequestId = c.Int(nullable: false),
                        productId = c.Int(nullable: false),
                        Description = c.String(),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductMasters", t => t.productId, cascadeDelete: true)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.receiptEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        receiptID = c.Int(nullable: false),
                        receiptDate = c.DateTime(),
                        accountGroupId = c.Int(nullable: false),
                        accountLedgerId = c.Int(nullable: false),
                        siteDetailsId = c.Int(nullable: false),
                        givenBy = c.String(),
                        collectBy = c.String(),
                        approvedBy = c.String(),
                        preparedBy = c.String(),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        remarks = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountGroupMasters", t => t.accountGroupId, cascadeDelete: true)
               // .ForeignKey("dbo.AccountLedgerMasters", t => t.accountLedgerId, cascadeDelete: true)
                .ForeignKey("dbo.SiteDetails", t => t.siteDetailsId, cascadeDelete: true)
                .Index(t => t.accountGroupId)
                .Index(t => t.accountLedgerId)
                .Index(t => t.siteDetailsId);
            
            CreateTable(
                "dbo.ToolsMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ToolsName = c.String(),
                        UOM = c.String(),
                        CategoryId = c.Int(nullable: false),
                        openingStock = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CategoryMasters", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SelectUserType = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        isDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdateBy = c.String(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToolsMasters", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.receiptEntries", "siteDetailsId", "dbo.SiteDetails");
            DropForeignKey("dbo.receiptEntries", "accountLedgerId", "dbo.AccountLedgerMasters");
            DropForeignKey("dbo.receiptEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropForeignKey("dbo.PurchaseRequestTables", "productId", "dbo.ProductMasters");
            DropForeignKey("dbo.PurchaseOrderTables", "productId", "dbo.ProductMasters");
            DropForeignKey("dbo.PurchaseEntryTables", "productId", "dbo.ProductMasters");
            DropForeignKey("dbo.PurchaseEntries", "SupplierId", "dbo.SupplierMasters");
            DropForeignKey("dbo.PurchaseEntries", "SiteDetails_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.PurchaseEntries", "PurchaseOrder_ID", "dbo.PurchaseOrders");
            DropForeignKey("dbo.PurchaseOrders", "SupplierId", "dbo.SupplierMasters");
            DropForeignKey("dbo.PurchaseOrders", "SiteDetails_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.PurchaseOrders", "PurchaseRequest_ID", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequests", "SupplierId", "dbo.SupplierMasters");
            DropForeignKey("dbo.SupplierMasters", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.PurchaseRequests", "SiteDetails_ID", "dbo.SiteDetails");
            DropForeignKey("dbo.PurchaseRequests", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.PurchaseOrders", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.PurchaseEntries", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.ProductMasters", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.paymentEntries", "siteDetailsId", "dbo.SiteDetails");
            DropForeignKey("dbo.paymentEntries", "accountLedgerId", "dbo.AccountLedgerMasters");
            DropForeignKey("dbo.paymentEntries", "accountGroupId", "dbo.AccountGroupMasters");
            DropForeignKey("dbo.EmployeeMasters", "DesignationId", "dbo.DesignationMasters");
            DropForeignKey("dbo.EmployeeMasters", "CategoryId", "dbo.CategoryMasters");
            DropForeignKey("dbo.AccountLedgerMasters", "AccountGroupID", "dbo.AccountGroupMasters");
            DropIndex("dbo.ToolsMasters", new[] { "CategoryId" });
            DropIndex("dbo.receiptEntries", new[] { "siteDetailsId" });
            DropIndex("dbo.receiptEntries", new[] { "accountLedgerId" });
            DropIndex("dbo.receiptEntries", new[] { "accountGroupId" });
            DropIndex("dbo.PurchaseRequestTables", new[] { "productId" });
            DropIndex("dbo.PurchaseOrderTables", new[] { "productId" });
            DropIndex("dbo.PurchaseEntryTables", new[] { "productId" });
            DropIndex("dbo.SupplierMasters", new[] { "CategoryId" });
            DropIndex("dbo.PurchaseRequests", new[] { "SiteDetails_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseRequests", new[] { "CategoryId" });
            DropIndex("dbo.PurchaseOrders", new[] { "SiteDetails_ID" });
            DropIndex("dbo.PurchaseOrders", new[] { "PurchaseRequest_ID" });
            DropIndex("dbo.PurchaseOrders", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseOrders", new[] { "CategoryId" });
            DropIndex("dbo.PurchaseEntries", new[] { "SiteDetails_ID" });
            DropIndex("dbo.PurchaseEntries", new[] { "PurchaseOrder_ID" });
            DropIndex("dbo.PurchaseEntries", new[] { "SupplierId" });
            DropIndex("dbo.PurchaseEntries", new[] { "CategoryId" });
            DropIndex("dbo.ProductMasters", new[] { "CategoryId" });
            DropIndex("dbo.paymentEntries", new[] { "siteDetailsId" });
            DropIndex("dbo.paymentEntries", new[] { "accountLedgerId" });
            DropIndex("dbo.paymentEntries", new[] { "accountGroupId" });
            DropIndex("dbo.EmployeeMasters", new[] { "CategoryId" });
            DropIndex("dbo.EmployeeMasters", new[] { "DesignationId" });
            DropIndex("dbo.AccountLedgerMasters", new[] { "AccountGroupID" });
            DropTable("dbo.Users");
            DropTable("dbo.ToolsMasters");
            DropTable("dbo.receiptEntries");
            DropTable("dbo.PurchaseRequestTables");
            DropTable("dbo.PurchaseOrderTables");
            DropTable("dbo.PurchaseEntryTables");
            DropTable("dbo.SupplierMasters");
            DropTable("dbo.PurchaseRequests");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseEntries");
            DropTable("dbo.ProductMasters");
            DropTable("dbo.SiteDetails");
            DropTable("dbo.paymentEntries");
            DropTable("dbo.masterTbls");
            DropTable("dbo.Errorlogs");
            DropTable("dbo.EmployeeMasters");
            DropTable("dbo.DesignationMasters");
            DropTable("dbo.CompanyDetails");
            DropTable("dbo.CategoryMasters");
            DropTable("dbo.AccountLedgerMasters");
            DropTable("dbo.AccountGroupMasters");
        }
    }
}
