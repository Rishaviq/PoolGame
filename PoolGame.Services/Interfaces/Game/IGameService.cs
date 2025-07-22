using PoolGame.Services.DTOs.Game.Request;
using PoolGame.Services.DTOs.Game.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Interfaces.Game
{
    public interface IGameService
    {
        public Task<CreateGameResponse> CreateGame();
        public Task<GetGameResponse> GetGameById(int gameId);
        public Task<GetGamesListResponse> GetGameByDate(DateTime gameDate);


      
    }
}
