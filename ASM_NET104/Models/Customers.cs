using System;
using System.Collections.Generic;

namespace ASM_NET104.Models
{
    public partial class Customers
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
    }
}
