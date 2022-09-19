using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.ViewModels.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required!!!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!!!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory!!!")]
        public string Password { get; set; }
    }
}
