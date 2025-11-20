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

        [Required]
        public int UserId { get; set; }
        public string? ProfileName { get; set; }


        [Range(0, 15, ErrorMessage = "Player cant make more shots than available balls")]
        public int ShotsMade { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Attempts should be a positive number")]
        public int ShotsAttempted { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Number of hand ball fouls should be a positive number")]
        public int HandBalls { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Number of fouls commited should be a positive number")]
        public int Fouls { get; set; }


        [Range(0, 15, ErrorMessage = "Best streak should be between 0 and 15, becouse there are no more balls than 15")]
        public int BestStreak { get; set; }
    }
}
