using PoolGame.Services.DTOs.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.PlayerStat.Response
{
    public class GetPlayerGameStatsResponse : ResponseDTO
    {
        public List<PlayerStatDTO> PlayerStats { get; set; } = new List<PlayerStatDTO>();
        public GameDTO GameInfo { get; set; } = new GameDTO();

    }
}
