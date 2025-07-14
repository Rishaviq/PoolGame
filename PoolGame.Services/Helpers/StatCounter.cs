using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Helpers
{
    public class StatCounter
    {
        public int TotalGamesPlayed { get; set; } = 0;
        public int TotalGamesWon { get; set; } = 0;
        public int TotalGamesLost { get; set; } = 0;
        public int TotalShotsMade { get; set; } = 0;
        public int TotalShotsAttempted { get; set; } = 0;
        public int BestStreak { get; set; } = 0;
        public int TotalHandBalls { get; set; } = 0;
        public int TotalFouls { get; set; } = 0;
    }
}
