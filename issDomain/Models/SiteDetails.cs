using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
   public class SiteDetails
    {
        public int ID { get; set; }
        public int ProjectId;
        public int SiteId;
        public DateTime? ProjectDate { get; set; }
        public string ProjectName { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string MobileNo1 { get; set;}
        public string MobileNo2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public DateTime? DateOfStart { get; set; }
        public string ReffBy { get; set; }
        public string ReffContactNo { get; set; }
        public string Branch { get; set; }
        public string NatureOfWork { get; set; }
        public string WorkStatus { get; set; }
        public string ClientType { get; set; }
        public string SupervisorName { get; set; }
        public string Length { get; set; }
        public string Breath { get; set; }
        public string Sqrft { get; set; }
        public string BuildupArea { get; set; }
        public string FSI { get; set; }
        public string NoOfFloor { get; set; }
        public string CostPerSqrft { get; set; }
        public string TotalCost { get; set; }
        public string AdditionalCost { get; set; }
        public string NetCost { get; set; }
        public string NoOfColoumn { get; set; }
        public string MeasurmentOfBelt { get; set; }
        public string payment1 { get; set; }
        public string payment2 { get; set; }
        public string payment3 { get; set; }
        public string payment4 { get; set; }
        public string payment5 { get; set; }
        public string payment1Description { get; set; }
        public string payment2Description { get; set; }
        public string payment3Description { get; set; }
        public string payment4Description { get; set; }
        public string payment5Description { get; set; }
        public string TypeOfWork { get; set; }
        public string WorkDescription { get; set; }
        public string Field { get; set; }
        public string Department { get; set; }
        public string TenderName { get; set; }
        public DateTime? TenderDate { get; set; }
        public string TenderEMDCost { get; set; }
        public string WorkAllocated { get; set; }
        public DateTime? AgreementTime { get; set; }
        public string SecurityDeposit { get; set; }
        public string AdditionalSD { get; set; }
        public string BusinessFund { get; set; }
        public string Reduction { get; set; }
        public string TenderDD { get; set; }
        public string BankName { get; set; }
        public string DDNo { get; set; }
        public DateTime? DDDate { get; set; }
        public DateTime? RefundDate { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
