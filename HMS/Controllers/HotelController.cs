using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS.Models;
using HMS.Models.Entity;

namespace HMS.Controllers
{
    public class HotelController : Controller
    {
        private readonly AppDbContext _context;

        public HotelController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(hotel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hotel hotel)
        {
            _context.Add(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Hotel hotel)
        {
            _context.Update(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var hotel = await _context.Hotels
                .FirstOrDefaultAsync(m => m.Id == id);
            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Hotel hotel)
        {

            _context.Hotels.Remove(hotel);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
