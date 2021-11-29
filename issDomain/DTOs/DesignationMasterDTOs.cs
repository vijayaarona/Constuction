﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace issDomain.DTOs
{
    class DesignationMasterDTOs
    {
        public string DesignationName { get; set; }
        public string Remarks { get; set; }
        public bool isDeleted { get; set; } = false;
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
