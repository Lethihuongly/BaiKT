using System.ComponentModel.DataAnnotations;

namespace Cau4.Models
{
    public class HangHoa
    {
        [Key]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Mã hàng hóa phải đúng 9 ký tự")]
        public string MaHangHoa { get; set; }

        [Required(ErrorMessage = "Tên hàng hóa không được để trống")]
        public string TenHangHoa { get; set; }

        public int SoLuong { get; set; }

        public string GhiChu { get; set; } // Thêm cột ghi_chu từ Câu 3
    }
}
