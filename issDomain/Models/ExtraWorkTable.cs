using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.Models
{
    public class ExtraWorkTable
    {
        public int Id { get; set; }
        public int extraWorkId { get; set; }
        public string Description { get; set; }
        public decimal length { get; set; }
        public decimal Breath { get; set; }
        public decimal Deapth { get; set; }
        public int Nos { get; set; }
        public decimal quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal amount { get; set; }
        public int umoId { get; set; }
        public virtual ProductMaster umo { get; set; }
        public bool isDeleted { get; set; } = false;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? UpdatedDate { get; set; }
        public DateTime? ExtraDate { get; set; }
    }
}
