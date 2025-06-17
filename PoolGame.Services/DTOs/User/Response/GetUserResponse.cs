using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.User.Responses
{
    public class GetUserResponse : ResponseDTO
    {
        public UserDTO? User { get; set; }
    }
}
