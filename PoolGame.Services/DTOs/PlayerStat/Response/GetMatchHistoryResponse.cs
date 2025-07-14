using PoolGame.Services.DTOs.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.PlayerStat.Response
{
    public class GetMatchHistoryResponse
    {
        public List<PlayerGameDTO> PlayerGames { get; set; } = new List<PlayerGameDTO>();
    }
}
