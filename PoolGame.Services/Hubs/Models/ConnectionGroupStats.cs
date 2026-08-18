using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Hubs.Models
{
    public class ConnectionGroupStats
    {
        public ConnectionGroupStats() {
           PlayersGameInfo =  new List<PlayerInfo>();
            PlayerInfoLock = new object();
        }

        public ConnectionGroupStats(string groupName):this() {
          
            this.GroupName = groupName;
        }

        public required string GroupName { get; set; }
        public List<PlayerInfo> PlayersGameInfo { get; set; }

        public object PlayerInfoLock { get; }
        
    }
}
