using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class ToolsTransfer
    {
        public int Id { get; set; }
        public DateTime? TDate { get; set; }
        public string Type { get; set; }
        public string FLocation { get; set; }
        public String TLocation { get; set; }
        public int ToolsId { get; set; }
        public virtual ToolsMaster ToolsName { get; set; }
        public int qty { get; set; }
        public String AuthPerson { get; set; }
public bool isDeleted { get; set; } = false;
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
