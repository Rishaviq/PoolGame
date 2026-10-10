using Microsoft.EntityFrameworkCore;
using PoolGame.EFCore.Data;
using PoolGame.EFCore.Models;
using PoolGame.Services.DTOs;
using PoolGame.Services.DTOs.Game;
using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.DTOs.PlayerStat;
using PoolGame.Services.DTOs.PlayerStat.Request;
using PoolGame.Services.DTOs.PlayerStat.Response;
using PoolGame.Services.Exceptions;
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
        private readonly PoolGameDbContext _DbContext;
        public PlayerStatService(PoolGameDbContext gameDbContext)
        {
            _DbContext = gameDbContext;
        }



        public async Task<GetMatchHistoryResponse> GetMatchHistoryOfUser(int userId)
        {

            GetMatchHistoryResponse response = new GetMatchHistoryResponse();


            response.PlayerGames = await (
                from playerStat in _DbContext.PlayerStats
                join game in _DbContext.Games
                on playerStat.GameId equals game.GameId
                where playerStat.UserId == userId
                orderby game.GameDate descending
                select new PlayerGameDTO
                {
                    GameId = playerStat.GameId,
                    PlayerId = playerStat.UserId,
                    GameDate = game.GameDate,
                    GameIsDraw = game.GameIsDraw,
                    IsPlayerWinner = playerStat.IsWinner
                }
                ).ToListAsync();

            return response;
        }




        public async Task<GetPersonalStatsResponse> GetPersonalStatsOfUser(int userId)
        {


            List<EFCore.Models.PlayerStat> playerStats = new List<EFCore.Models.PlayerStat>();
            playerStats = await _DbContext.PlayerStats.Where(x => x.UserId == userId).ToListAsync();
            GetPersonalStatsResponse response = FillUniversalStats(new GetPersonalStatsResponse(), playerStats);


            List<EFCore.Models.PlayerStat> finishedGamesList = await _DbContext.PlayerStats
                .Where(x => x.UserId == userId && x.Game.GameIsDraw == false)
                .ToListAsync();




            response = FillOutcomeDependantStats(response, finishedGamesList);

            response.ProfileName = await (from User in _DbContext.Users where User.UserId == userId select User.Username).FirstOrDefaultAsync();
            return response;
        }





        public async Task<GetPlayerGameStatsResponse> GetPlayerStatsForGame(int gameId)
        {
            GetPlayerGameStatsResponse response = new GetPlayerGameStatsResponse();

            var gameInfo = await _DbContext.Games.Where(x => x.GameId == gameId).SingleOrDefaultAsync();
            if (gameInfo is null)
            {
                return new GetPlayerGameStatsResponse
                {
                    IsSuccesful = false,
                    Message = "No such game exists"
                };
            }


            var playerStats = await _DbContext.PlayerStats.Where(x => x.Game.GameId == gameId).ToListAsync();
            if (playerStats.Count <= 0)
            {
                return new GetPlayerGameStatsResponse
                {
                    IsSuccesful = false,
                    Message = "No player stats found for this game."
                };
            }


            response.GameInfo = new GameDTO
            {
                GameId = gameInfo.GameId,
                GameDate = gameInfo.GameDate,
                GameIsDraw = gameInfo.GameIsDraw
            };

            foreach (var statBlock in playerStats)
            {


                response.PlayerStats.Add(new PlayerStatDTO
                {
                    UserId = statBlock.UserId,
                    ShotsMade = statBlock.ShotsMade ?? 0,
                    ShotsAttempted = statBlock.ShotsAttempted ?? 0,
                    HandBalls = statBlock.HandBalls ?? 0,
                    Fouls = statBlock.Fouls ?? 0,
                    BestStreak = statBlock.BestStreak ?? 0,
                    IsWinner = statBlock.IsWinner,
                    UserName = statBlock.User.Username
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



            _DbContext.PlayerStats.Add((new EFCore.Models.PlayerStat
            {
                GameId = request.GameId,
                UserId = request.UserId,
                ShotsMade = request.ShotsMade,
                ShotsAttempted = request.ShotsAttempted,
                HandBalls = request.HandBalls,
                Fouls = request.Fouls,
                BestStreak = request.BestStreak,
                IsWinner = request.IsWinner
            }));

            if (request.IsWinner)
            {

                EFCore.Models.Game? game = await _DbContext.Games.FindAsync(request.GameId);
                game.GameIsDraw = false;

            }
            await _DbContext.SaveChangesAsync();

            return new SaveStatsResponse();//temp response until the method is implemented
        }



        public GetPersonalStatsResponse FillUniversalStats(GetPersonalStatsResponse response, List<EFCore.Models.PlayerStat> playerStats)
        {
            // fills the stats that are not dependant of if the game ended. Like Shot accuracy can be filled and be accurate even if the game is not finished.
            //should pass a list with all the game stats that can be found for the user
            StatCounter statCounter = new StatCounter();
            foreach (var stat in playerStats)
            {
                statCounter.TotalGamesPlayed++;
                statCounter.TotalShotsMade += stat.ShotsMade ?? 0;
                statCounter.TotalShotsAttempted += stat.ShotsAttempted ?? 0;
                if (statCounter.BestStreak < stat.BestStreak)
                {
                    statCounter.BestStreak = stat.BestStreak ?? 0;
                }
            }
            if (statCounter.TotalGamesPlayed <= 0) { throw new UserValidationException("Player Has no games Played"); }

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




        public GetPersonalStatsResponse FillOutcomeDependantStats(GetPersonalStatsResponse response, List<EFCore.Models.PlayerStat> playerStats)
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

                statCounter.TotalHandBalls += stat.HandBalls ?? 0;
                statCounter.TotalFouls += stat.Fouls ?? 0;
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
                response.AverageHandBalls = statCounter.TotalGamesPlayed > 0 ? statCounter.TotalHandBalls / statCounter.TotalGamesPlayed : 0;
            }
            if (response.AverageFouls is null)
            {
                response.AverageFouls = statCounter.TotalGamesPlayed > 0 ? statCounter.TotalFouls / statCounter.TotalGamesPlayed : 0;
            }



            return response;
        }



        public async Task<ResponseDTO> ValidateStatRequest(SaveStatsRequest request)
        {
            if (request.ShotsMade > request.ShotsAttempted)
            {
                return new ResponseDTO("Player can't make more shots than attempted");
            }

            EFCore.Models.Game? game = await _DbContext.Games.FindAsync(request.GameId);
            if (game is null)
            {
                return new ResponseDTO("Game does not exist");
            }
            EFCore.Models.User? user = await _DbContext.Users.FindAsync(request.UserId);
            if (user is null)
            {
                return new ResponseDTO("User does not exist");
            }

            var playerAlreadyHasStats = await _DbContext.PlayerStats.AnyAsync(x => x.GameId == request.GameId && x.UserId == request.UserId);
            if (playerAlreadyHasStats)
            {
                return new ResponseDTO("Player stats for this game already exist");
            }


            return new ResponseDTO();
        }

        //public async Task<GetLeaderboardResponse> GetLeaderBoard()
        //{
        //    HashSet<int> drawGamesId = new HashSet<int>();
        //    await foreach (var game in _gameRepository.RetrieveCollectionAsync(new GameFilter { GameIsDraw = true }))
        //    {
        //        drawGamesId.Add(game.GameId);
        //    }

        //    Dictionary<int, LeaderBoardEntryDTO> stats = new Dictionary<int, LeaderBoardEntryDTO>();
        //    await foreach (var playerStat in _playerStatRepository.RetrieveCollectionAsync(new PlayerStatFilter()))
        //    {
        //        stats.TryAdd(playerStat.UserId, new LeaderBoardEntryDTO());
        //        stats[playerStat.UserId].TotalGames++;

        //        if (!drawGamesId.Contains(playerStat.GameId))
        //        {
        //            //checking if the game is draw and saving the result
        //            if (playerStat.IsWinner)
        //            {
        //                stats[playerStat.UserId].GamesWon++;
        //            }
        //            else { stats[playerStat.UserId].GamesLost++; }
        //        }

        //    }
        //    GetLeaderboardResponse response = new GetLeaderboardResponse();
        //    foreach (var stat in stats)
        //    {
        //        if (stat.Value.GamesWon + stat.Value.GamesLost > 0)
        //        {
        //            stat.Value.UserId = stat.Key;
        //            stat.Value.WinRate = (decimal)stat.Value.GamesWon / (stat.Value.GamesWon + stat.Value.GamesLost) * 100;
        //            stat.Value.UserName = _userRepository.RetrieveAsync(stat.Value.UserId).Result.ProfileName; //username/prodile name mixup becouse i dont want to change it everywhere
        //            response.LeaderboardEntries.Add(stat.Value);
        //        }
        //    }
        //    response.LeaderboardEntries = response.LeaderboardEntries.OrderByDescending(entry => entry.WinRate).ThenByDescending(entry => entry.TotalGames).ToList();
        //    return response;
        //}

        public async Task<GetLeaderboardResponse> GetLeaderBoard()
        {//done by claude TODO: rewrite it myself
            
            var grouped = await (
                from ps in _DbContext.PlayerStats
                join g in _DbContext.Games on ps.GameId equals g.GameId
                group new { ps.IsWinner, g.GameIsDraw } by ps.UserId into grp
                select new
                {
                    UserId = grp.Key,
                    TotalGames = grp.Count(),
                    GamesWon = grp.Count(x => x.IsWinner && !x.GameIsDraw),
                    GamesLost = grp.Count(x => !x.IsWinner && !x.GameIsDraw)
                })
                .Where(x => x.GamesWon + x.GamesLost > 0)
                .AsNoTracking()
                .ToListAsync();

           
            var userIds = grouped.Select(x => x.UserId).ToList();
            var userNames = await _DbContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .AsNoTracking()
                .ToDictionaryAsync(u => u.UserId, u => u.ProfileName);

            var entries = grouped
                .Select(x => new LeaderBoardEntryDTO
                {
                    UserId = x.UserId,
                    UserName = userNames.GetValueOrDefault(x.UserId),
                    TotalGames = x.TotalGames,
                    GamesWon = x.GamesWon,
                    GamesLost = x.GamesLost,
                    WinRate = (decimal)x.GamesWon / (x.GamesWon + x.GamesLost) * 100
                })
                .OrderByDescending(e => e.WinRate)
                .ThenByDescending(e => e.TotalGames)
                .ToList();

            return new GetLeaderboardResponse { LeaderboardEntries = entries };
        }

    }
}
