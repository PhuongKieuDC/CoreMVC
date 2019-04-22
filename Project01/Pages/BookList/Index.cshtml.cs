using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project01.Models;

namespace Project01.Pages.BookList
{
    public class IndexModel : PageModel
    {
        private readonly DemoDbContext _context;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<Book> Books { get; set; }

        public IndexModel(DemoDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            Books = await _context.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            Message = "Book deleted successfull";
            return RedirectToPage("Index");
        }
    }
}