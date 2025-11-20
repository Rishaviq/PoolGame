using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Hubs.Models
{
    public class ConnInfo
    {
        public string? ConnectionId { get; set; }
        public int? PlayerId { get; set; }
        public required string GroupName { get; set; }
    }
}
