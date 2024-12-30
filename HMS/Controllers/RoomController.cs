using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS.Models;
using HMS.Models.Entity;
using HMS.ViewModel;

namespace HMS.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Rooms.Include(r => r.Hotel);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        public async Task<IActionResult> Create()
        {
            var hotels = await _context.Hotels.ToListAsync();
            RoomViewModel viewModel = new RoomViewModel
            {
                Hotels = hotels
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomViewModel viewModel)
        {
            var room = new Room
            {
                HotelID = viewModel.HotelID,
                RoomNumber = viewModel.RoomNumber,
                Type = viewModel.Type,
                PricePerNight = viewModel.PricePerNight,
                IsAvailable = viewModel.IsAvailable,

            };
            await _context.AddAsync(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var room = await _context.Rooms.FindAsync(id);

            RoomViewModel viewModel = new RoomViewModel
            {
                ID = room.ID,
                HotelID = room.HotelID,
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                PricePerNight = room.PricePerNight,
                IsAvailable = room.IsAvailable,
                Hotels = await _context.Hotels.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoomViewModel roomView)
        {
            Room room = new Room
            {
                ID = roomView.ID,
                HotelID = roomView.HotelID,
                RoomNumber = roomView.RoomNumber,
                Type = roomView.Type,
                PricePerNight = roomView.PricePerNight,
                IsAvailable = roomView.IsAvailable
            };
            _context.Update(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            var room = await _context.Rooms
                .Include(r => r.Hotel)
                .FirstOrDefaultAsync(m => m.ID == id);
            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
