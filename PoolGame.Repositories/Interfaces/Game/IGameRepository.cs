using PoolGame.Repositories.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.Game
{
    public interface IGameRepository: IBaseRepository<Models.Game, GameFilter, GameUpdate>
    {
    }
}
