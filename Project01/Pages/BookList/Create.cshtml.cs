using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project01.Models;

namespace Project01.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly DemoDbContext _context;

        [TempData]
        public string Message { get; set; }

        public CreateModel(DemoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _context.Add(Book);
            await _context.SaveChangesAsync();
            Message = "Book has been created successful";
            return RedirectToPage("Index");
        }
    }
}