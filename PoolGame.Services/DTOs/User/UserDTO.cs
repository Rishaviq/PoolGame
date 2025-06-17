using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.User
{
    public class UserDTO
    {
        public int UserId { get; set; }     
        public required string Username { get; set; }
        public required string ProfileName { get; set; }
    }
}
