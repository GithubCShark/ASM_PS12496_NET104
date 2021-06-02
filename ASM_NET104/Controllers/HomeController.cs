using ASM_NET104.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment_NET104.Controllers
{
    public class HomeController : Controller
    {
        private readonly Web_SalesContext _context;
        public HomeController(Web_SalesContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var web_SalesContext = _context.Products.Include(p => p.CategoryCodeNavigation);
            return View(await web_SalesContext.ToListAsync());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}