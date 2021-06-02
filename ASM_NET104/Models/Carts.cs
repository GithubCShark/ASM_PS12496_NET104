using System;
using System.Collections.Generic;

namespace ASM_NET104.Models
{
    public partial class Carts
    {
        public string CartCode { get; set; }
        public string Phone { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }
        public double? Total { get; set; }
    }
}
