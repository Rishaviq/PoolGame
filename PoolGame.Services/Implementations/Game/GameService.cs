using PoolGame.Repositories.Interfaces.Game;
using PoolGame.Services.DTOs.Game;
using PoolGame.Services.DTOs.Game.Request;
using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.Interfaces.Game;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Implementations.Game
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public async Task<CreateGameResponse> CreateGame()
        {
            
            CreateGameResponse response = new CreateGameResponse();
            response.GameId= await _gameRepository.CreateAsync(new Models.Game
            {
                GameDate = DateTime.Now,
                GameIsDraw = true // Default value, can be changed later based on game logic
            });
            Log.Information("Someone created a game with game Id:{gameId}",response.GameId);
            return response;
        }

        public async Task<GetGamesListResponse> GetGameByDate(DateTime gameDate)
        {
            Log.Information("Someone wants to see all the games played on {gameDate}",gameDate);
            GetGamesListResponse response = new GetGamesListResponse();
            GameFilter filter = new GameFilter
            {
                GameDate = gameDate.Date
            };
            await foreach (var game in _gameRepository.RetrieveCollectionAsync(filter))
            {
                response.Games.Add(MapToGameDTO(game));
            }
            return response;
        }

        public async Task<GetGameResponse> GetGameById(int gameId)
        {
            Models.Game game = await _gameRepository.RetrieveAsync(gameId);

            return new GetGameResponse
            {
                Game = MapToGameDTO(game),
                IsSuccesful = true
            };
        }
        public GameDTO MapToGameDTO(Models.Game game)
        {
            return new GameDTO
            {
                GameId = game.GameId,
                GameDate = game.GameDate,
                GameIsDraw = game.GameIsDraw
            };
        }
    }
}
