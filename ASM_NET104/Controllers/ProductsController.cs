using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASM_NET104.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ASM_NET104.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Web_SalesContext _context;
        private readonly IHostingEnvironment _hostEnviroment;

        public ProductsController(Web_SalesContext context, IHostingEnvironment hostEnviroment)
        {
            _context = context;
            this._hostEnviroment = hostEnviroment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var web_SalesContext = _context.Products.Include(p => p.CategoryCodeNavigation);
            return View(await web_SalesContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.CategoryCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "CategoryCode", "CategoryCode");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Description,ProductImage,Price,CategoryCode")] Products products)
        {
            if (ModelState.IsValid)
            {
                string rootpath = _hostEnviroment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(products.imageFile.FileName);
                products.ProductImage = fileName = products.imageFile.FileName;
                string path = Path.Combine(rootpath + "/images/", fileName);

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await products.imageFile.CopyToAsync(fs);
                }

                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "CategoryCode", "CategoryCode", products.CategoryCode);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "CategoryCode", "CategoryCode", products.CategoryCode);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ProductName,Description,ProductImage,Price,CategoryCode")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryCode"] = new SelectList(_context.Categories, "CategoryCode", "CategoryCode", products.CategoryCode);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.CategoryCodeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
