using System;
using System.Collections.Generic;

namespace ASM_NET104.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public string CategoryCode { get; set; }
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
