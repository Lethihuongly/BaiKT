using Microsoft.EntityFrameworkCore;
using GoodsManagement.Models;

namespace GoodsManagement.Data
{
    public class GoodsDbContext : DbContext
    {
        public GoodsDbContext(DbContextOptions<GoodsDbContext> options) : base(options) { }

        public DbSet<Goods> Goods { get; set; }
    }
}
