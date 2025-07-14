using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.PlayerStat.Response
{
    public class GetPersonalStatsResponse : ResponseDTO
    {
        public decimal? PlayerWinRate { get; set; }
        public int? TotalGamesPlayed { get; set; }
        public int? TotalGamesWon { get; set; }
        public int? TotalGamesLost { get; set; }
        public decimal? ShotSuccessRate { get; set; }
        public int? TotalShotsMade { get; set; }
        public int? TotalShotsAttempted { get; set; }
        public decimal? AverageHandBalls { get; set; }
        public decimal? AverageFouls { get; set; }
        public int? BestStreak { get; set; }

    }
}
