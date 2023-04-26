using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        public string password { get; set; }
    }
}
