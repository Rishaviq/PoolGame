using Microsoft.EntityFrameworkCore;
using PoolGame.EFCore.Data;
using PoolGame.Services.DTOs.Game;
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

        private readonly PoolGameDbContext _dbContext;
        public GameService(PoolGameDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<CreateGameResponse> CreateGame()
        {
            EFCore.Models.Game newGame = new EFCore.Models.Game { GameDate = DateTime.Now, GameIsDraw = true };
            _dbContext.Games.Add(newGame);
            await _dbContext.SaveChangesAsync();

            return new CreateGameResponse { GameId = newGame.GameId };
        }

        public async Task<GetGamesListResponse> GetGameByDate(DateTime gameDate)
        {
            GetGamesListResponse response = new GetGamesListResponse();

            foreach (var game in await _dbContext.Games.Where(x => x.GameDate.Date == gameDate.Date).ToListAsync())
            {

                response.Games.Add(MapToGameDTO(game));
            }

            return response;
        }

        public async Task<GetGameResponse> GetGameById(int gameId)
        {

            EFCore.Models.Game? game = await _dbContext.Games.Where(x => x.GameId == gameId).FirstOrDefaultAsync();
            if (game != null)
                return new GetGameResponse
                {
                    Game = MapToGameDTO(game)

                };

            else
            {
                return new GetGameResponse { IsSuccesful = false, Message = "There is no game with this ID" };
            }
        }
        public GameDTO MapToGameDTO(EFCore.Models.Game game)
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
