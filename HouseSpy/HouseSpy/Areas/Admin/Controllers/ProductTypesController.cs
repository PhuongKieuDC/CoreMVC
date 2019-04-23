using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseSpy.Data;
using HouseSpy.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseSpy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.ProductTypes.ToList());
        }

        
        public IActionResult Create()
        {
            return View();
        }

        //create action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if(ModelState.IsValid)
            {
                _context.ProductTypes.Add(productTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }
    }
}