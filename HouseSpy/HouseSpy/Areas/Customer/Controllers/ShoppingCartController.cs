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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> lstCartItem = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            ShoppingCartVM.Appointments.AppointmentDate = ShoppingCartVM.Appointments.AppointmentDate.AddHours(ShoppingCartVM.Appointments.AppointmentTime.Hour).AddMinutes(ShoppingCartVM.Appointments.AppointmentTime.Minute);
            Appointments appointments = ShoppingCartVM.Appointments;
            _context.Appointments.Add(appointments);
            _context.SaveChanges();

            int apponitmentId = appointments.Id;

            foreach(int productId in lstCartItem)
            {
                ProductsSelectedForAppointment productsSelectedForAppointment = new ProductsSelectedForAppointment()
                {
                    AppointmentId = apponitmentId,
                    ProductId = productId
                };
                _context.ProductsSelectedForAppointments.Add(productsSelectedForAppointment);
                _context.SaveChanges();
            }

            _context.SaveChanges();

            //set bag null when click button submit
            lstCartItem = new List<int>();
            HttpContext.Session.Set("ssShoppingCart", lstCartItem);
            return RedirectToAction("AppointmentConfirm", "ShoppingCart", new { Id = apponitmentId});
        }

        public IActionResult remove(int id)
        {
            List<int> lstCartItems = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if (lstCartItems.Count > 0)
            {
                if(lstCartItems.Contains(id))
                {
                    lstCartItems.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", lstCartItems);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AppointmentConfirm(int id)
        {
            ShoppingCartVM.Appointments = _context.Appointments.Where(x => x.Id == id).FirstOrDefault();
            List<ProductsSelectedForAppointment> objProducts = _context.ProductsSelectedForAppointments.Where(x => x.AppointmentId == id).ToList();
            
            foreach (ProductsSelectedForAppointment obj in objProducts)
            {
                ShoppingCartVM.Products.Add(_context.Products.Include(x => x.ProductTypes).Include(x => x.SpecialTag).Where(x => x.Id == obj.ProductId).FirstOrDefault());
            }

            return View(ShoppingCartVM);
        }
    }
}