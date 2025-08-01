using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.PlayerStat
{
    public class LeaderBoardEntryDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public decimal WinRate { get; set; }
        public int TotalGames {  get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
    }
}
