using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_NET104.Models
{
    public class ProductModel
    {
        private Web_SalesContext db = new Web_SalesContext();
        public List<Products> FindAll()
        {

            var a = db.Products.ToList();
            return a;

        }

        public Products Find(long id)
        {
            var a = db.Products.Find(id);
            return a;
        }
    }
}
