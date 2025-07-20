using System.ComponentModel.DataAnnotations;

namespace PoolGame.WebAPI.Models.LogIn.Requests
{
    public class LoginRequestDTO
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
