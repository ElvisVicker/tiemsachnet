using System.ComponentModel.DataAnnotations;

namespace tiemsach.ViewModels
{
    public class NguoiDungVM
    {
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]
        public long QuyenId { get; set; }
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]
        public required string Hoten { get; set; }
        public bool Gioitinh { get; set; }
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]
        public bool Vaitro { get; set; }
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]




        [RegularExpression(@"0[123456789]\d{8}", ErrorMessage = "Phone number invalid")]
        public string Sodienthoai { get; set; }
        public string? Diachi { get; set; }
        public bool Tinhtrang { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn hình ảnh.")]
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nhập đầy đủ thông tin")]
        public string Password { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        }
    }
    
