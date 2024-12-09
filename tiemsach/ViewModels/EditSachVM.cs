using System.ComponentModel.DataAnnotations;

namespace tiemsach.ViewModels
{
    public class EditSachVM
    {
        [Required(ErrorMessage = "Không được bỏ trốnng tên sách!")]
        [StringLength(255, ErrorMessage = "Tên sách không được quá 255 ký tự")]
        public string Ten { get; set; }
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
        public long TacgiaId { get; set; }
        public long LoaisachId { get; set; }

        [Required(ErrorMessage = "Không được bỏ trốnng mô tả!")]
        public string Mota { get; set; }

        public double Gianhap { get; set; }

        public double Giaxuat { get; set; }

        public int Soluong { get; set; }

        public bool Tinhtrang { get; set; }

    }
}