using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM_NET104.Models
{
    public partial class Products
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public double? Price { get; set; }
        public string CategoryCode { get; set; }

        [NotMapped]
        [DisplayName("Tải hình lên")]
        public IFormFile imageFile { get; set; }

        public Categories CategoryCodeNavigation { get; set; }
    }
}
