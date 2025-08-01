using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.PlayerStat.Response
{
    public class GetLeaderboardResponse : ResponseDTO
    {
        public List<LeaderBoardEntryDTO> LeaderboardEntries { get; set; } = new List<LeaderBoardEntryDTO>();
    }
}
