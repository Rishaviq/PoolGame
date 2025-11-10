using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using PoolGame.Services.DTOs.HubDTOs.Request;
using PoolGame.Services.Hubs.Helpers;
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
        private static readonly ConcurrentDictionary<string, ConnInfo> _connectionPages = new();
        public async Task JoinGame(JoinGameRequest request)
        {
            string groupName = request.GameId.ToString();
            _connectionPages[Context.ConnectionId] = new ConnInfo { PlayerId = request.UserId.ToString(), GroupName = groupName };
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("AddPlayer", request);

        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {

            if (_connectionPages.TryRemove(Context.ConnectionId, out var connInfo))
            {

                await Clients.OthersInGroup(connInfo.GroupName).SendAsync("RemovePlayer", connInfo.PlayerId);


                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connInfo.GroupName);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateLiveStat(LiveStatUpdateRequest request) {
            await Clients.OthersInGroup(_connectionPages[Context.ConnectionId].GroupName).SendAsync("UpdateUser", request);
        }
    }
}
