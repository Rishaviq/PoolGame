using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccesful { get; set; } = true;
        public string? Message { get; set; }
    }
}
