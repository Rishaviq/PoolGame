using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using PoolGame.Services.DTOs.HubDTOs.Request;
using PoolGame.Services.Hubs.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Hubs.Implementations
{
    public class LiveGameHub : Hub
    {
        private static readonly ConcurrentDictionary<int, ConnInfo> _connectionPerId = new();
        private static readonly ConcurrentDictionary<string, ConnInfo> _connectionPerClientId = new();

        public async Task JoinGame(JoinGameRequest request)

        {
            Console.WriteLine(Context.ConnectionId + request.UserId + " joined game");
            try
            {
                string groupName = request.GameId.ToString();
                if (_connectionPerId.TryAdd(request.UserId, new ConnInfo { ConnectionId = Context.ConnectionId, GroupName = groupName })
                    && _connectionPerClientId.TryAdd(Context.ConnectionId, new ConnInfo { PlayerId = request.UserId, GroupName = groupName })
                    )
                {

                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                    await Clients.OthersInGroup(groupName).SendAsync("AddPlayer", request);
                    await Clients.OthersInGroup(groupName).SendAsync("SendUpdate");

                }
                else
                {
                    _connectionPerId.TryRemove(request.UserId, out var connInfo);
                    _connectionPerClientId.TryRemove(connInfo.ConnectionId, out var value);
                    // await Groups.RemoveFromGroupAsync(connInfo.ConnectionId, connInfo.GroupName);

                    _connectionPerId.TryAdd(request.UserId, new ConnInfo { ConnectionId = Context.ConnectionId, GroupName = groupName });
                    _connectionPerClientId.TryAdd(Context.ConnectionId, new ConnInfo { PlayerId = request.UserId, GroupName = groupName });
                    await Clients.OthersInGroup(groupName).SendAsync("AddPlayer", request);
                    await Clients.OthersInGroup(groupName).SendAsync("SendUpdate");


                }

                foreach (var connection in _connectionPerId)
                {
                    Console.WriteLine(connection.Key + "  " + connection.Value.ConnectionId);
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
