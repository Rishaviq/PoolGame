using PoolGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Helpers.Interfaces
{
    public interface ITokenProvider
    {
        public string getUsernameFromToken(string token);
        public string CreateToken(User user);
    }
}
