using System.ComponentModel.DataAnnotations;
using tiemsach.Data;

namespace tiemsach.ViewModels
{
    public class NhanVien_ViewModel
    {
        [Required]
        public long id { get; set; }

        [Required]
        public string hoten { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password does not match")]
        public string confirm_password { get; set; }

        [Required]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid phone number")]
        public string sodienthoai { get; set; }

        [Required] public string diachi { get; set; }

        [Required] public bool gioitinh { get; set; }

        [Required] public string image { get; set; }

        [Required] public string vitri { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public List<Quyen> Quyens { get; set; }
        [Required] public long quyen_id { get; set; }

        public NhanVien_ViewModel() { }
        public NhanVien_ViewModel(Nguoidung user, string vitri, DateTime? created_at, DateTime? updated_at)
        {
            id = user.Id;
            hoten = user.Hoten;
            email = user.Email;
            diachi = user.Diachi;
            sodienthoai = user.Sodienthoai;
            gioitinh = user.Gioitinh;
            image = user.Image;
            quyen_id = user.QuyenId;
            password = user.Password;
            this.vitri = vitri;
            this.created_at = created_at;
            this.updated_at = updated_at;
        }
    }
}
