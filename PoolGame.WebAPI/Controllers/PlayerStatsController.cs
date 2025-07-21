using Azure;
using Microsoft.AspNetCore.Mvc;
using PoolGame.Services.DTOs.PlayerStat.Response;
using PoolGame.Services.Interfaces.PlayerStat;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolGame.WebAPI.Controllers
{
    [Route("player")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerStatService _playerStatService;
        public PlayerStatsController(IPlayerStatService playerStatService)
        {
            _playerStatService = playerStatService;
        }

        // GET /player/{id}/stats
        [HttpGet("{id}/stats")]
        public async Task<ActionResult<GetPersonalStatsResponse>> GetStatsOfPlayer(int id)
        {
            GetPersonalStatsResponse response = await _playerStatService.GetPersonalStatsOfUser(id);
            if (response.IsSuccesful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message });
            }
        }

        // GET /player/{id}/history
        [HttpGet("{id}/history")]
        public async Task<ActionResult<GetMatchHistoryResponse>> GetMatchHistory(int id)
        {
           var response = await _playerStatService.GetMatchHistoryOfUser(id);
            if (response.IsSuccesful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message });
            }
        }
    }
}



