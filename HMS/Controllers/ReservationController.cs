

using HMS.ViewModel;

namespace HMS.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Reservations.Include(r => r.Customer).Include(r => r.Room);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public async Task<IActionResult> Create()
        {
            var availableRoom = await _context.Rooms.Where(x => x.IsAvailable == true).ToListAsync();
            var customer = await _context.Customers.ToListAsync();

            ReservationViewModel reservationViewModel = new ReservationViewModel
            {
                AvailableRoom = availableRoom,
                Customer = customer
            };
            return View(reservationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel reservationViewModel)
        {
            var ci = reservationViewModel.CheckIn;
            var co = reservationViewModel.CheckOut;
            var ppn = _context.Rooms.Where(x => x.ID == reservationViewModel.RoomId).Select(x => x.PricePerNight).FirstOrDefault();
            var total = (co - ci).TotalDays * ppn;
            Reservation reservation = new Reservation
            {
                RoomId = reservationViewModel.RoomId,
                CustomerId = reservationViewModel.CustomerId,
                CheckIn = ci,
                CheckOut = co,
                TotalPrice = total
            };


            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var availableRoom = await _context.Rooms.Where(x => x.IsAvailable == true).ToListAsync();
            var customer = await _context.Customers.ToListAsync();

            var res = await _context.Reservations.FindAsync(id);
            var reservation = new ReservationViewModel
            {
                RoomId = res.RoomId,
                CheckIn = res.CheckIn,
                CheckOut = res.CheckIn,
                CustomerId = res.CustomerId,
                TotalPrice = res.TotalPrice,
                AvailableRoom = availableRoom,
                Customer = customer

            };
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationViewModel reservation)
        {
            var res = await _context.Reservations.FindAsync(reservation.Id);
            res.RoomId = reservation.RoomId;
            res.CheckIn = reservation.CheckIn;
            res.CheckOut = reservation.CheckOut;
            res.CustomerId = reservation.CustomerId;
            res.TotalPrice = reservation.TotalPrice;
            _context.Update(res);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
