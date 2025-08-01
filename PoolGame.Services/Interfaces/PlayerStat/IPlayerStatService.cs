using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.DTOs.PlayerStat.Request;
using PoolGame.Services.DTOs.PlayerStat.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Interfaces.PlayerStat
{
    public interface IPlayerStatService
    {
        public Task<GetMatchHistoryResponse> GetMatchHistoryOfUser(int userId);
        public Task<SaveStatsResponse> SaveStats(SaveStatsRequest saveStatsRequest);

        public Task<GetPersonalStatsResponse> GetPersonalStatsOfUser(int userId);

        public Task<GetPlayerGameStatsResponse> GetPlayerStatsForGame(int gameId);

        public Task<GetLeaderboardResponse> GetLeaderBoard();
    }
}
