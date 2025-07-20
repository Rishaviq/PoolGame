using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PoolGame.Services.DTOs.User.Requests;
using PoolGame.Services.DTOs.User.Response;
using PoolGame.Services.DTOs.User.Responses;
using PoolGame.Services.Interfaces.User;
using PoolGame.WebAPI.Models.LogIn.Requests;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolGame.WebAPI.Controllers
{

    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IUserService _userService;
        public LogInController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("LogIn")]
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LoginRequestDTO request)
        {
            LoginResponse loginResponse = await _userService.Login(new Services.DTOs.User.Requests.LoginRequest
            {
                Username = request.Username,
                Password = request.Password
            });
            if (loginResponse.IsSuccesful)
            {
                return Ok(new { id = loginResponse.UserId, jwt = loginResponse.AuthToken });
            }
            else { return Unauthorized(new { message = loginResponse.Message }); }

        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                Services.DTOs.User.Requests.RegisterRequest InternalRegisterRequest = new Services.DTOs.User.Requests.RegisterRequest
                {
                    Username = request.Username,
                    UserPassword = request.UserPassword,
                    ProfileName = request.ProfileName
                };
                RegisterResponse registerResponse = await _userService.Register(InternalRegisterRequest);
                if (registerResponse.IsSuccesful)
                {
                    return Ok();
                }
                else { return BadRequest(new { message = registerResponse.Message }); }
            }
            else { return BadRequest(); }
        }

    }
}
