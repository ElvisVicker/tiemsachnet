
using System.ComponentModel.DataAnnotations;

namespace tiemsach.ViewModels
{
    public class RegisterVM
    {
    

        //[Key]
        public long Id { get; set; }

        public long QuyenId { get; set; } = 5;

        [Display(Name = "Ho ten")]
        [Required(ErrorMessage = "*")]
        public string Hoten { get; set; }

 
        public bool Gioitinh { get; set; } = false;


      
        public bool Vaitro { get; set; } = false;

        [RegularExpression(@"0[123456789]\d{8}", ErrorMessage = "Phone number invalid")]
        public string Sodienthoai { get; set; } = null!;

        public string? Diachi { get; set; } = null!;

        public bool Tinhtrang { get; set; }

        public string? Image { get; set; }

   
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

    
        public long DiachiId { get; set; }


    }
}
