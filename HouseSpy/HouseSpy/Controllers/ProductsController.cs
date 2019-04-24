using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseSpy.Data;
using HouseSpy.Models.ViewModel;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HouseSpy.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HostingEnvironment _hostingEnviroment;

        [BindProperty]
        public ProductsViewModel ProductsVM { get; set; }

        public ProductsController(ApplicationDbContext context, HostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnviroment = hostingEnvironment;
            ProductsVM = new ProductsViewModel()
            {
                ProductTypes = _context.ProductTypes.ToList(),
                SpecialTags = _context.SpecialTags.ToList(),
                Products = new Models.Products()
            };
        }

        public async Task<IActionResult> Index()
        {
            var product = _context.Products.Include(x => x.ProductTypes).Include(x => x.SpecialTag);
            return View(await product.ToListAsync());
        }

        //get create action method
        public IActionResult Create()
        {
            return View(ProductsVM);
        }

        //post create action method
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if(!ModelState.IsValid)
            {
                return View(ProductsVM);
            }
            _context.Products.Add(ProductsVM.Products);
            await _context.SaveChangesAsync();

            //Image being saved
            string webRootPath = _hostingEnviroment.WebRootPath;
            return View();
        }
    }
}