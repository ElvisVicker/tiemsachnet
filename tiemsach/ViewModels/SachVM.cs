using System.ComponentModel.DataAnnotations;

namespace tiemsach.ViewModels
{
    public class SachVM
    {
        [Required(ErrorMessage = "Không được bỏ trốnng tên sách!")]
        [StringLength(255, ErrorMessage = "Tên sách không được quá 255 ký tự")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "Không được bỏ trốnng hình ảnh!")]
        public IFormFile Image { get; set; }
        public int TacgiaId { get; set; }
        public int LoaisachId { get; set; }

        [Required(ErrorMessage = "Không được bỏ trốnng mô tả!")]
        public string Mota { get; set; }

    }
}