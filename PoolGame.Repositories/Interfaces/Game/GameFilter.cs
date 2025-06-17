using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.Game
{
    public class GameFilter
    {
        public SqlDateTime? GameDate { get; set; }
        public SqlBoolean? GameIsDraw { get; set; }
    }
}
