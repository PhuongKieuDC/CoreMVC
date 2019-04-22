using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project01.Models;

namespace Project01.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly DemoDbContext _context;

        public EditModel(DemoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; }

        [TempData]
        public string Message { get; set; }

        //Find idBook to show value in input
        public void OnGet(int id)
        {
            Book = _context.Books.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var BookFromDb =  await _context.Books.FindAsync(Book.Id);
                BookFromDb.Name = Book.Name;
                BookFromDb.ISBN = Book.ISBN;
                BookFromDb.Author = Book.Author;
                _context.Books.Update(BookFromDb);
                await _context.SaveChangesAsync();
                Message = "Book has been updated successfully";
                return RedirectToPage("Index");
            }
            return RedirectToPage("Index");
        }
    }
}