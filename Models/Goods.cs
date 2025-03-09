using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsManagement.Models
{
    [Table("Goods")]
    public class Goods
    {
        [Key]
        [StringLength(9, MinimumLength = 9)]
        public string MaHangHoa { get; set; } = null!;

        [Required]
        public string TenHangHoa { get; set; } = null!;

        public int SoLuong { get; set; }

        public string? GhiChu { get; set; }  // Thêm cột ghi chú
    }
}
