using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.ViewModels.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is Mandatory!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Mandatory!")]
        public string Password { get; set; }
    }
}
