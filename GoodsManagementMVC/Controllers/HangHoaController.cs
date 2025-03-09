using Cau4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cau4.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly GoodsDbContext _context;

        public HangHoaController(GoodsDbContext context)
        {
            _context = context;
        }

        // GET: HangHoa
        public async Task<IActionResult> Index()
        {
            var goods = await _context.Goods.ToListAsync();
            return View(goods);
        }

        // GET: HangHoa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HangHoa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHangHoa,TenHangHoa,SoLuong,GhiChu")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hangHoa);
        }

        // GET: HangHoa/Edit/ABC123456
        public async Task<IActionResult> Edit(string maHangHoa)
        {
            if (maHangHoa == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }

        // POST: HangHoa/Edit/ABC123456
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string maHangHoa, [Bind("MaHangHoa,TenHangHoa,SoLuong,GhiChu")] HangHoa hangHoa)
        {
            if (maHangHoa != hangHoa.MaHangHoa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hangHoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HangHoaExists(hangHoa.MaHangHoa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hangHoa);
        }

        // GET: HangHoa/Delete/ABC123456
        public async Task<IActionResult> Delete(string maHangHoa)
        {
            if (maHangHoa == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }

            return View(hangHoa);
        }

        // POST: HangHoa/Delete/ABC123456
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string maHangHoa)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa != null)
            {
                _context.Goods.Remove(hangHoa);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: HangHoa/UpdateNote/ABC123456
        public async Task<IActionResult> UpdateNote(string maHangHoa)
        {
            if (maHangHoa == null)
            {
                return NotFound();
            }

            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return View(hangHoa);
        }

        // POST: HangHoa/UpdateNote/ABC123456
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNote(string maHangHoa, [Bind("MaHangHoa,GhiChu")] HangHoa hangHoa)
        {
            if (maHangHoa != hangHoa.MaHangHoa)
            {
                return NotFound();
            }

            var existingHangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (existingHangHoa == null)
            {
                return NotFound();
            }

            existingHangHoa.GhiChu = hangHoa.GhiChu;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HangHoaExists(string maHangHoa)
        {
            return _context.Goods.Any(e => e.MaHangHoa == maHangHoa);
        }
    }
}
