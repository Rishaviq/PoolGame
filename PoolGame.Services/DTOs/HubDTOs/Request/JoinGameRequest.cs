using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PoolGame.Services.DTOs.HubDTOs.Request
{
    public class JoinGameRequest
    {
        [Required]
        
        public int UserId { get; set; }

        [Required]
        
        public int GameId { get; set; }

       
        public string? ProfileName { get; set; }



    }
}
