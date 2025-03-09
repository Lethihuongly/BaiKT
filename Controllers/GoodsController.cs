using Microsoft.AspNetCore.Mvc;
using GoodsManagement.Data;
using GoodsManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly GoodsDbContext _context;
        public GoodsController(GoodsDbContext context) { _context = context; }

        // Lấy danh sách hàng hóa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> GetGoods()
        {
            return await _context.Goods.ToListAsync();
        }

        // Lấy hàng hóa theo mã (truyền qua route hoặc query string)
        [HttpGet("{id}")]
        public async Task<ActionResult<Goods>> GetGoodsById(string id)
        {
            var goods = await _context.Goods.FindAsync(id);
            return goods == null ? NotFound() : goods;
        }

        [HttpPost]
        public async Task<ActionResult<Goods>> CreateGoods(Goods goods)
        {
            _context.Goods.Add(goods);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGoodsById), new { id = goods.MaHangHoa }, goods);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoods(string id, Goods goods)
        {
            if (id != goods.MaHangHoa) return BadRequest();
            _context.Entry(goods).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoods(string id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null) return NotFound();
            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
