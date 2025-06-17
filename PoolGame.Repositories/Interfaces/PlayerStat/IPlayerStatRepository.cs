using PoolGame.Repositories.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.PlayerStat
{
    public interface IPlayerStatRepository : IBaseRepository<Models.PlayerStat, PlayerStatFilter, PlayerStatUpdate>
    {
    }
    
}
