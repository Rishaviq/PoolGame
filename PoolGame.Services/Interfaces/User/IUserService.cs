using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoolGame.Services.DTOs.User.Requests;
using PoolGame.Services.DTOs.User.Response;
using PoolGame.Services.DTOs.User.Responses;

namespace PoolGame.Services.Interfaces.User
{
    public interface IUserService
    {
        public Task<LoginResponse> Login(LoginRequest loginRequest);
        public Task<GetUserResponse> GetUser(int userId);
        public Task<RegisterResponse> Register(RegisterRequest registerRequest);
    }
}
