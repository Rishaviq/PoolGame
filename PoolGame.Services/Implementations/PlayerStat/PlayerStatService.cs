using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.DTOs.PlayerStat.Request;
using PoolGame.Services.DTOs.PlayerStat.Response;
using PoolGame.Services.Interfaces.PlayerStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Implementations.PlayerStat
{
    public class PlayerStatService : IPlayerStatService
    {
        public Task<GetGamesListReponse> GetGamesByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GetPersonalStatsResponse> GetPersonalStatsOfUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<GetPlayerGameStatsResponse> GetPlayerStatsForGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<SaveStatsResponse> SaveStats(SaveStatsRequest saveStatsRequest)
        {
            throw new NotImplementedException();
        }
    }
}
