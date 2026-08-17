using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Tls;
using PoolGame.Models;
using PoolGame.Services.DTOs.HubDTOs.Request;
using PoolGame.Services.Hubs.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Hubs.Implementations
{
    public class LiveGameHub : Hub
    {
        //private static readonly ConcurrentDictionary<int, ConnInfo> _connectionPerId = new();
        //private static readonly ConcurrentDictionary<string, List<int> > _gamesPerConnection = new();
        private static readonly ConcurrentDictionary<string, ConnectionGroupStats> _gamesByGameId = new();


        public async Task JoinGame(JoinGameRequest request)

        {
            try
            {
            Console.WriteLine(Context.ConnectionId + request.PlayerId + " joined game");
                string groupName = request.GameId.ToString();
                string previousConnection = "";
                Task? updateGame = null;

                _gamesByGameId.TryAdd(groupName, new ConnectionGroupStats { GroupName = groupName });
                if (_gamesByGameId.TryGetValue(groupName, out var game))
                {
                    PlayerInfo? player;
                    lock (game.PlayerInfoLock)
                    {
                        player = game.PlayersGameInfo.Find(x => x.PlayerId == request.PlayerId);
                        if (player == null)
                        {
                            player = new PlayerInfo
                            {
                                ConnectionId = Context.ConnectionId,
                                PlayerId = request.PlayerId,
                                ProfileName = request.ProfileName,
                                ShotsAttempted = request.Stats.ShotsAttempted,
                                ShotsMade = request.Stats.ShotsMade,
                                Fouls = request.Stats.Fouls,
                                HandBalls = request.Stats.HandBalls,
                                BestStreak = request.Stats.BestStreak
                            };

                            game.PlayersGameInfo.Add(player);
                            updateGame = Clients.OthersInGroup(groupName).SendAsync("AddNewPlayer", player);
                        }
                        else
                        {
                            previousConnection = player.ConnectionId ?? "";

                            player.ConnectionId = Context.ConnectionId;
                            player.PlayerId = request.PlayerId;
                            player.ProfileName = request.ProfileName;
                            player.ShotsMade = request.Stats.ShotsMade;
                            player.ShotsAttempted = request.Stats.ShotsAttempted;
                            player.Fouls = request.Stats.Fouls;
                            player.HandBalls = request.Stats.HandBalls;
                            player.BestStreak = request.Stats.BestStreak;

                            updateGame = Clients.OthersInGroup(groupName).SendAsync("UpdatePlayer", player);
                        }


                    }


                    await Groups.RemoveFromGroupAsync(previousConnection, groupName);
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    await updateGame;
                    await Clients.Caller.SendAsync("CreateGame", game.PlayersGameInfo);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        private async Task RemovePlayer(string connectionId)
        {
            try
            {
                Task? sendRemovePlayerRequest = null;
                Task? removePlayerFromGroup = null;
                string groupName = "";

                foreach (var game in _gamesByGameId)
                {
                    lock (game.Value.PlayerInfoLock)
                    {
                        var possiblePlayersList = game.Value.PlayersGameInfo.Where(player => player.ConnectionId == connectionId).ToList();
                        if (possiblePlayersList.IsNullOrEmpty()) continue;

                        game.Value.PlayersGameInfo.RemoveAll(player => possiblePlayersList.Contains(player));
                        groupName = game.Value.GroupName;

                        sendRemovePlayerRequest = Clients.OthersInGroup(groupName).SendAsync("RemovePlayer", possiblePlayersList[0].PlayerId);
                        removePlayerFromGroup = Groups.RemoveFromGroupAsync(connectionId, groupName);




                        break;
                    }
                }

                if (_gamesByGameId.TryGetValue(groupName, out var currentgame))
                {
                    lock (currentgame.PlayerInfoLock)
                    {
                        if (currentgame.PlayersGameInfo.Count <= 0) { _gamesByGameId.TryRemove(groupName, out currentgame); }
                    }
                }

                if (sendRemovePlayerRequest == null || removePlayerFromGroup == null) return;

                await sendRemovePlayerRequest;
                await removePlayerFromGroup;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LeaveGame()
        {
            await RemovePlayer(Context.ConnectionId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine(Context.ConnectionId);
            await RemovePlayer(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateLiveStats(LiveStatUpdateRequest request)
        {
            try
            {
                lock (_gamesByGameId[request.GameId.ToString()].PlayerInfoLock)
                {


                    var playerStats = _gamesByGameId[request.GameId.ToString()].PlayersGameInfo.FirstOrDefault(player => player.PlayerId == request.PlayerId);
                    if (playerStats == null) return;


                    playerStats.ShotsAttempted = request.Stats.ShotsAttempted;
                    playerStats.ShotsMade = request.Stats.ShotsMade;
                    playerStats.BestStreak = request.Stats.BestStreak;
                    playerStats.Fouls = request.Stats.Fouls;
                    playerStats.HandBalls = request.Stats.HandBalls;
                }


                await Clients.OthersInGroup(request.GameId.ToString()).SendAsync("UpdateUser", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
