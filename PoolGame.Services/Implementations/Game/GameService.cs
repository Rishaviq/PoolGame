using PoolGame.Services.DTOs.Game.Request;
using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.Interfaces.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Implementations.Game
{
    public class GameService : IGameService
    {
        public Task<CreateGameResponse> CreateGame(CreateGameRequest createGameRequest)
        {
            throw new NotImplementedException();
        }

        public Task<GetGameResponse> GetGameByDate(DateTime gameDate)
        {
            throw new NotImplementedException();
        }

        public Task<GetGameResponse> GetGameById(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
