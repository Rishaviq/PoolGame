using PoolGame.Services.Hubs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.HubDTOs.Request
{
    public class LiveStatUpdateRequest
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public string? ProfileName { get; set; }

       public LiveStats Stats { get; set; }
    }
}
