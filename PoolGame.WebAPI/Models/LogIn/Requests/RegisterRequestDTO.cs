using System.ComponentModel.DataAnnotations;

namespace PoolGame.WebAPI.Models.LogIn.Requests
{
    public class RegisterRequestDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "User name cannot be longer than 50 characters.")]
        public required string Username { get; set; }

        [StringLength(50, ErrorMessage = "Profile name cannot be longer than 50 characters.")]
        public string? ProfileName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Password can only be up to 50 chars")]
        public required string UserPassword { get; set; }
    }
}
