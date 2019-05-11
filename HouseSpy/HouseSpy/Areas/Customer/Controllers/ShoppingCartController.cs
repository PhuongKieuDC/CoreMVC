using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseSpy.Data;
using HouseSpy.Extensions;
using HouseSpy.Models;
using HouseSpy.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseSpy.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                Products = new List<Models.Products>()
            };
        }

        //get index shopping cart
        public async Task<IActionResult> Index()
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (listShoppingCart.Count > 0)
            {
                foreach(int cartitem in listShoppingCart)
                {
                    Products products = _context.Products.Include(x => x.SpecialTag).Include(x => x.ProductTypes).Where(x => x.Id == cartitem).FirstOrDefault();
                    ShoppingCartVM.Products.Add(products);
                }
            }
            return View(ShoppingCartVM);
        }
    }
}