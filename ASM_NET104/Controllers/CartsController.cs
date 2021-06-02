using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_NET104.Models;
using ASM_NET104.Helpers;

namespace ASM_NET104.Controllers
{
    [Route("cart")]
    public class CartsController : Controller
    {
        private readonly Web_SalesContext _context;

        public CartsController(Web_SalesContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartDetail>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            return View(await _context.Carts.ToListAsync());
        }

        [Route("buy/{id}")]
        public IActionResult Buy(long id)
        {
            ProductModel productModel = new ProductModel();
            if (SessionHelper.GetObjectFromJson<List<CartDetail>>(HttpContext.Session, "cart") == null)
            {
                List<CartDetail> cart = new List<CartDetail>();
                cart.Add(new CartDetail { Product = productModel.Find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartDetail> cart = SessionHelper.GetObjectFromJson<List<CartDetail>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartDetail { Product = productModel.Find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<CartDetail> cart = SessionHelper.GetObjectFromJson<List<CartDetail>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(long id)
        {
            List<CartDetail> cart = SessionHelper.GetObjectFromJson<List<CartDetail>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
