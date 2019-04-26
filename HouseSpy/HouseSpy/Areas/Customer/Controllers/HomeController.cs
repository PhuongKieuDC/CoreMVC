using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HouseSpy.Models;
using HouseSpy.Data;
using Microsoft.EntityFrameworkCore;
using HouseSpy.Extensions;

namespace HouseSpy.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var listproduct = await _context.Products.Include(x => x.ProductTypes).Include(x => x.SpecialTag).ToListAsync();
            return View(listproduct);
        }

        public async Task<IActionResult> Details(int id)
        {
            
            var product = await _context.Products.Include(x => x.ProductTypes).Include(x => x.SpecialTag).Where(x => x.Id == id).SingleOrDefaultAsync();
           
            return View(product);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int id)
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (listShoppingCart == null)
            {
                listShoppingCart = new List<int>();
            }
            listShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", listShoppingCart);
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult Remove(int id)
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (listShoppingCart.Count > 0)
            {
                if (listShoppingCart.Contains(id))
                {
                    listShoppingCart.Remove(id);
                }
            }
            HttpContext.Session.Set("ssShoppingCart", listShoppingCart);
            return RedirectToAction(nameof(Index));
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
