using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using issDomain.Models;

namespace issConstructions.Models
{
    public class issDB : DbContext
    {
        public issDB()
            : base("name=ISSModel")
        {
        }
        public System.Data.Entity.DbSet<AccountGroupMaster> accountGroupMasters { get; set; }
        public System.Data.Entity.DbSet<AccountLedgerMaster> accountLedgerMasters { get; set; }
        public System.Data.Entity.DbSet<CategoryMaster> categoryMasters { get; set; }
        public System.Data.Entity.DbSet<DesignationMaster> designationMasters { get; set; }
        public System.Data.Entity.DbSet<ProductMaster> productMasters { get; set; }
        public System.Data.Entity.DbSet<ToolsMaster> toolsMasters { get; set; }
        public System.Data.Entity.DbSet<CompanyDetails> companyDetails { get; set; }
        public System.Data.Entity.DbSet<EmployeeMaster> employeeMaster { get; set; }
        public System.Data.Entity.DbSet<Errorlog> errorlog { get; set; }
        public System.Data.Entity.DbSet<SupplierMaster> supplierMasters { get; set; }
        public System.Data.Entity.DbSet<Users> users { get; set; }
        public System.Data.Entity.DbSet<SiteDetails> siteDetails { get; set; }
        public System.Data.Entity.DbSet<PurchaseRequest> purchaseRequest { get; set; }
        public System.Data.Entity.DbSet<PurchaseRequestTable> purchaseRequestTables { get; set; }
        public System.Data.Entity.DbSet<PurchaseOrder>PurchaseOrders{ get; set; }
        public System.Data.Entity.DbSet<PurchaseOrderTable> purchaseOrderTables { get; set; }
        public System.Data.Entity.DbSet<PurchaseEntry> purchaseEntries { get; set; }
        public System.Data.Entity.DbSet<PurchaseEntryTable> purchaseEntryTables { get; set; }
        public System.Data.Entity.DbSet<paymentEntry> paymentEntries { get; set; }
        public System.Data.Entity.DbSet<receiptEntry> receiptEntries { get; set; }
        public System.Data.Entity.DbSet<masterTbl> masterTbls { get; set; }
        public System.Data.Entity.DbSet<Issues> issues { get; set; }
        public System.Data.Entity.DbSet<IssueTable> issueTables { get; set; }
        public System.Data.Entity.DbSet<tblStock> tblStocks { get; set; }
        public System.Data.Entity.DbSet<RateWork> rateWorks { get; set; }
        public System.Data.Entity.DbSet<RateWorkTable> rateWorkTables { get; set; }
        public System.Data.Entity.DbSet<ExtraWork> extraWorks { get; set; }
        public System.Data.Entity.DbSet<ExtraWorkTable> extraWorkTables { get; set; }
        public System.Data.Entity.DbSet<Godown> godowns { get; set; }
        public System.Data.Entity.DbSet<ToolsTransfer> toolsTransfers { get; set; }
        public System.Data.Entity.DbSet<TblType> tblTypes  { get; set; }

    }
}