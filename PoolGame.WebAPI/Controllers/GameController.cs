using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoolGame.Services.DTOs.Game.Response;
using PoolGame.Services.Interfaces.Game;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolGame.WebAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        // GET: <GameController>
        [HttpGet]
        public async Task<ActionResult<GetGamesListResponse>> GetGameByDate([FromBody]DateTime date)
        {
            GetGamesListResponse response = await _gameService.GetGameByDate(date);
            if (response.IsSuccesful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message });
            }
        }

        // GET <GameController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetGameResponse>> GetGameById(int id)
        {
            GetGameResponse response = await _gameService.GetGameById(id);
            if (response.IsSuccesful)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message });
            }
        }

        // POST <GameController>
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<CreateGameResponse>> CreateGame()
        {
           CreateGameResponse response=await _gameService.CreateGame();

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
