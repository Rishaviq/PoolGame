using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Interfaces.PlayerStat
{
   public class PlayerStatUpdate
    {
        public SqlBoolean? IsWinner { get; set; }
        public SqlInt32? ShotsMade { get; set; }
        public SqlInt32? ShotsAttempted { get; set; }
        public SqlInt32? HandBalls { get; set; }
        public SqlInt32? Fouls { get; set; }
        public SqlInt32? BestStreak { get; set; }
    }
}
