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
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialTagController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.SpecialTags.ToList());
        }

        //get create action method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Add(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }

        //get edit action method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTags.FindAsync(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //post edit action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpecialTag specialTag)
        {
            if (id != specialTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.SpecialTags.Update(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        //get details action method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTags.FindAsync(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //get delete action method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTags.FindAsync(id);

            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        //post delete action method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var specialTag = await _context.SpecialTags.FindAsync(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            _context.SpecialTags.Remove(specialTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}