using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Models
{
   public class PlayerStat
    {
        public int StatId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public bool IsWinner { get; set; }
        public int ShotsMade { get; set; }
        public int ShotsAttempted { get; set; }
        public int HandBalls { get; set; }
        public int Fouls { get; set; }
        public int BestStreak { get; set; }
    }
}
