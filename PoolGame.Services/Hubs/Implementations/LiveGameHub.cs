using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Tls;
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
            Console.WriteLine(Context.ConnectionId + request.UserId + " joined game");
            try
            {
                string groupName = request.GameId.ToString();
                string previousConnection = "";
                Task? updateGame=null;

                _gamesByGameId.TryAdd(groupName, new ConnectionGroupStats { GroupName = groupName });
                if (_gamesByGameId.TryGetValue(groupName, out var game))
                {
                    PlayerInfo? player;
                    lock (game.PlayerInfoLock)
                    {
                        player = game.PlayersGameInfo.Find(x => x.PlayerId == request.UserId);
                        if (player == null)
                        {
                            player = new PlayerInfo
                            {
                                ConnectionId = Context.ConnectionId,
                                PlayerId = request.UserId,
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
                            player.PlayerId = request.UserId;
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
                    await Clients.Caller.SendAsync("CreateGame", game);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

        public async Task LeaveGame()
        {
            try
            {
                string connId = Context.ConnectionId;

                if (_connectionPerClientId.TryRemove(Context.ConnectionId, out var connInfo))
                {
                    _connectionPerId.TryRemove(connInfo.PlayerId ?? 0, out var value);

                    await Clients.OthersInGroup(connInfo.GroupName).SendAsync("RemovePlayer", connInfo.PlayerId);


                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, connInfo.GroupName);

                    Console.WriteLine(Context.ConnectionId.ToString() + " has left the group");

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task UpdateLiveStats(LiveStatUpdateRequest request)
        {
            try
            {
                await Clients.OthersInGroup(_connectionPerClientId[Context.ConnectionId].GroupName).SendAsync("UpdateUser", request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
