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
        public int userId;

       public LiveStats Stats { get; set; }
    }
}
