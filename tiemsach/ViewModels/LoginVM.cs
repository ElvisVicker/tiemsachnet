using System.ComponentModel.DataAnnotations;

namespace tiemsach.ViewModels
{
    public class LoginVM
    {

        [Display(Name = "Email")]
        [Required(ErrorMessage ="*")]

        public string Email { get; set; }



        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



    }
}
