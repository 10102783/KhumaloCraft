using System.ComponentModel.DataAnnotations;

namespace KhumaloCraft.Models
{
    public class RegisterModel
    {
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage ="The passsword and confimation password doesnot match")]
            public string ConfirmPassword { get; set;}
        }
    }
}
