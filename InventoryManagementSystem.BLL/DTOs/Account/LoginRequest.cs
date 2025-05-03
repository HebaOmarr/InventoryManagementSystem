using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.API.DTOs.Account
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }

    }
}
