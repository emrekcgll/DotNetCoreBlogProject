using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your surname.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter your username.")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter your a correct email.")]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your confirm password.")]
        [Compare("Password", ErrorMessage = "Password is not confirm")]
        public string ConfirmPassword { get; set; }
    }
}
