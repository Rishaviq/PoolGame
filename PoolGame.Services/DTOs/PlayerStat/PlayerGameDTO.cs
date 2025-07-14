using PoolGame.Services.DTOs.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.PlayerStat
{
    public class PlayerGameDTO : GameDTO
    {
        public int PlayerId { get; set; }
        public bool IsPlayerWinner { get; set; }
    }
}
