using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoolGame.Services.DTOs.PlayerStat.Request;
using PoolGame.Services.DTOs.PlayerStat.Response;
using PoolGame.Services.Helpers.Interfaces;
using PoolGame.Services.Interfaces.PlayerStat;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolGame.WebAPI.Controllers
{
    [Authorize]
    [Route("player")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerStatService _playerStatService;
        private readonly ITokenProvider _tokenProvider;
        public PlayerStatsController(IPlayerStatService playerStatService,ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
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


        // GET /player/{id}/history
        [HttpGet("game-stats/{gameId}")]
        public async Task<ActionResult<GetPlayerGameStatsResponse>> GetStatsForGame(int gameid)
        {
            var response = await _playerStatService.GetPlayerStatsForGame(gameid);
            if (response.IsSuccesful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message });
            }
        }

        // POST /player/stats/save
        [HttpPost]
        [Route("stats/save")]
        public async Task<ActionResult<SaveStatsResponse>> SaveStats([FromBody]SaveStatsRequest request)
        {
            request.UserId =_tokenProvider.getUserIdFromToken(Request.Headers["Authorization"].ToString());
            SaveStatsResponse response= await _playerStatService.SaveStats(request);

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



