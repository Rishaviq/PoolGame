using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.Game
{
    public class GameDTO
    {
        public int GameId { get; set; }
        [Required]
        public DateTime GameDate { get; set; }
        [Required]
        public bool GameIsDraw { get; set; }
    }
}
