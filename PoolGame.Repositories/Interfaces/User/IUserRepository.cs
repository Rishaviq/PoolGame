using PoolGame.Repositories.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.User
{
   public interface IUserRepository : IBaseRepository<Models.User, UserFilter, UserUpdate>
    {
    }
}
