using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class ToolsTransfer
    {
        public int Id { get; set; }
        public DateTime? TDate { get; set; }
        public int GodownId { get; set; }
        public int SNameId { get; set; }
        public int TypeId { get; set; }
        public  virtual TblType Type { get; set; }
        public int ToolsId { get; set; }
        public virtual ToolsMaster Tools { get; set; }
        public int qty { get; set; }
        public String AuthPerson { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
