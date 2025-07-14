using PoolGame.Repositories.Interfaces.Game;
using PoolGame.Repositories.Interfaces.PlayerStat;
using PoolGame.Repositories.Interfaces.User;
using PoolGame.Services.DTOs;
using PoolGame.Services.DTOs.Game;
using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.DTOs.PlayerStat;
using PoolGame.Services.DTOs.PlayerStat.Request;
using PoolGame.Services.DTOs.PlayerStat.Response;
using PoolGame.Services.Helpers;
using PoolGame.Services.Interfaces.PlayerStat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Implementations.PlayerStat
{
    public class PlayerStatService : IPlayerStatService
    {
        private readonly IPlayerStatRepository _playerStatRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        public PlayerStatService(IPlayerStatRepository playerStatRepository, IGameRepository gameRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _playerStatRepository = playerStatRepository;
            _gameRepository = gameRepository;
        }



        public async Task<GetMatchHistoryResponse> GetMatchHistoryOfUser(int userId)
        {
            PlayerStatFilter filter = new PlayerStatFilter
            {
                UserId = userId
            };
            GetMatchHistoryResponse response = new GetMatchHistoryResponse();


            await foreach (var playerStat in _playerStatRepository.RetrieveCollectionAsync(filter))
            {
                response.PlayerGames.Add(new PlayerGameDTO
                {
                    GameId = playerStat.GameId,
                    PlayerId = playerStat.UserId,
                    GameDate = _gameRepository.RetrieveAsync(playerStat.GameId).Result.GameDate,
                    GameIsDraw = _gameRepository.RetrieveAsync(playerStat.GameId).Result.GameIsDraw,
                    IsPlayerWinner = playerStat.IsWinner
                });
            }

            response.PlayerGames = response.PlayerGames.OrderByDescending(x => x.GameDate).ToList();
            return response;
        }




        public async Task<GetPersonalStatsResponse> GetPersonalStatsOfUser(int userId)
        {
            List<Models.PlayerStat> playerStats = new List<Models.PlayerStat>();
            await foreach (var playerStat in _playerStatRepository.RetrieveCollectionAsync(new PlayerStatFilter { UserId = userId }))
            {
                playerStats.Add(playerStat);
            }
            GetPersonalStatsResponse response = FillUniversalStats(new GetPersonalStatsResponse(), playerStats);


            HashSet<int> DrawnGamesList = new HashSet<int>();//list of all the games that ended in draw
            await foreach (var game in _gameRepository.RetrieveCollectionAsync(new GameFilter { GameIsDraw = true }))
            {
                DrawnGamesList.Add(game.GameId);
            }

            playerStats = playerStats.Where(x => !DrawnGamesList.Contains(x.GameId)).ToList();
            response = FillOutcomeDependantStats(response, playerStats);

            return response;
        }





        public async Task<GetPlayerGameStatsResponse> GetPlayerStatsForGame(int gameId)
        {
            GetPlayerGameStatsResponse response = new GetPlayerGameStatsResponse();

            var game = await _gameRepository.RetrieveAsync(gameId);

            response.gameInfo = new GameDTO
            {
                GameId = game.GameId,
                GameDate = game.GameDate,
                GameIsDraw = game.GameIsDraw
            };

            await foreach (var playerStat in _playerStatRepository.RetrieveCollectionAsync(new PlayerStatFilter { GameId = gameId }))
            {
                //Add a check to see if the player have only one set of stats for the game even if the database should not allow that to happen
                var user = await _userRepository.RetrieveAsync(playerStat.UserId);

                response.PlayerStats.Add(new PlayerStatDTO
                {
                    UserId = playerStat.UserId,
                    ShotsMade = playerStat.ShotsMade,
                    ShotsAttempted = playerStat.ShotsAttempted,
                    HandBalls = playerStat.HandBalls,
                    Fouls = playerStat.Fouls,
                    BestStreak = playerStat.BestStreak,
                    IsWinner = playerStat.IsWinner,
                    UserName = user.Username
                });
            }
            if (response.PlayerStats.Count < 1)
            {
                response.IsSuccesful = false;
                response.Message = "No player stats found for this game.";
            }

            return response;
        }




        public async Task<SaveStatsResponse> SaveStats(SaveStatsRequest request)
        {
            ResponseDTO validationResult = await ValidateStatRequest(request);
            if (!validationResult.IsSuccesful)
            {
                return new SaveStatsResponse { IsSuccesful = validationResult.IsSuccesful, Message = validationResult.Message };
            }
            await _playerStatRepository.CreateAsync(new Models.PlayerStat
            {
                GameId = request.GameId,
                UserId = request.UserId,
                ShotsMade = request.ShotsMade,
                ShotsAttempted = request.ShotsAttempted,
                HandBalls = request.HandBalls,
                Fouls = request.Fouls,
                BestStreak = request.BestStreak,
                IsWinner = request.IsWinner
            });


            return new SaveStatsResponse();//temp response until the method is implemented
        }



        public GetPersonalStatsResponse FillUniversalStats(GetPersonalStatsResponse response, List<Models.PlayerStat> playerStats)
        {
            // fills the stats that are not dependant of if the game ended. Like Shot accuracy can be filled and be accurate even if the game is not finished.
            //should pass a list with all the game stats that can be found for the user
            StatCounter statCounter = new StatCounter();
            foreach (var stat in playerStats)
            {
                statCounter.TotalGamesPlayed++;
                statCounter.TotalShotsMade += stat.ShotsMade;
                statCounter.TotalShotsAttempted += stat.ShotsAttempted;
                if (statCounter.BestStreak < stat.BestStreak)
                {
                    statCounter.BestStreak = stat.BestStreak;
                }
            }
            if (response.TotalGamesPlayed is null)
            {
                response.TotalGamesPlayed = statCounter.TotalGamesPlayed;
            }
            if (response.TotalShotsMade is null)
            {
                response.TotalShotsMade = statCounter.TotalShotsMade;
            }
            if (response.TotalShotsAttempted is null)
            {
                response.TotalShotsAttempted = statCounter.TotalShotsAttempted;
            }
            if (response.BestStreak is null)
            {
                response.BestStreak = statCounter.BestStreak;
            }
            if (response.ShotSuccessRate is null)
            {
                response.ShotSuccessRate = statCounter.TotalShotsAttempted > 0 ? ((decimal)statCounter.TotalShotsMade / statCounter.TotalShotsAttempted) * 100 : 0;

            }


            return response;
        }




        public GetPersonalStatsResponse FillOutcomeDependantStats(GetPersonalStatsResponse response, List<Models.PlayerStat> playerStats)
        {
            //for stats that can be accurate only if the game finished, like fouls and handballs
            //should pass a list without the games that ended in draw for the user. Than means only game that finished would be used to fill the stats

            StatCounter statCounter = new StatCounter();
            foreach (var stat in playerStats)
            {
                if (stat.IsWinner)
                {
                    statCounter.TotalGamesWon++;
                }
                else if (!stat.IsWinner)
                {
                    statCounter.TotalGamesLost++;
                }
                else { throw new Exception("Unfiltered data in player stats"); }

                statCounter.TotalHandBalls += stat.HandBalls;
                statCounter.TotalFouls += stat.Fouls;
                statCounter.TotalGamesPlayed++;
            }
            if (response.TotalGamesWon is null)
            {
                response.TotalGamesWon = statCounter.TotalGamesWon;
            }
            if (response.TotalGamesLost is null)
            {
                response.TotalGamesLost = statCounter.TotalGamesLost;
            }
            if (response.PlayerWinRate is null && statCounter.TotalGamesPlayed > 0)
            {
                response.PlayerWinRate = ((decimal)statCounter.TotalGamesWon / statCounter.TotalGamesPlayed) * 100;
            }
            else { response.PlayerWinRate = 0; }
            if (response.AverageHandBalls is null)
            {
                response.AverageHandBalls = statCounter.TotalHandBalls / statCounter.TotalGamesPlayed;
            }
            if (response.AverageFouls is null)
            {
                response.AverageFouls = statCounter.TotalFouls / statCounter.TotalGamesPlayed;
            }



            return response;
        }



        public async Task<ResponseDTO> ValidateStatRequest(SaveStatsRequest request)
        {
            if (request.ShotsMade > request.ShotsAttempted)
            {
                return new ResponseDTO("Player can't make more shots than attempted");
            }

            Models.Game game = await _gameRepository.RetrieveAsync(request.GameId);
            if (game is null)
            {
                return new ResponseDTO("Game does not exist");
            }
            Models.User user = await _userRepository.RetrieveAsync(request.UserId);
            if (user is null)
            {
                return new ResponseDTO("User does not exist");
            }
            await foreach (var stat in _playerStatRepository.RetrieveCollectionAsync(new PlayerStatFilter { GameId = request.GameId }))
            {
                if (stat.UserId == request.UserId)
                {
                    return new ResponseDTO("Player stats for this game already exist");
                }
            }


            return new ResponseDTO();
        }
    }
}
