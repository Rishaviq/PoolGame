using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.User.Responses
{
    public class LoginResponse : ResponseDTO
    {
        public int UserId { get; set; }
        public string? AuthToken { get; set; } = string.Empty;
    }
}
