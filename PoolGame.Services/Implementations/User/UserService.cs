using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoolGame.Repositories.Interfaces.User;
using PoolGame.Services.DTOs.User;
using PoolGame.Services.DTOs.User.Request;
using PoolGame.Services.DTOs.User.Requests;
using PoolGame.Services.DTOs.User.Response;
using PoolGame.Services.DTOs.User.Responses;
using PoolGame.Services.Helpers.Interfaces;
using PoolGame.Services.Interfaces.User;

namespace PoolGame.Services.Implementations.User
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository,IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }
        public async Task<GetUserResponse> GetUser(int userId)
        {
            try
            {
                Models.User user = await _userRepository.RetrieveAsync(userId);
                return new GetUserResponse
                {
                    User =
                          new UserDTO
                          {
                              UserId = user.UserId,
                              Username = user.Username,
                              ProfileName = user.ProfileName ?? string.Empty
                          }
                };
            }
            catch (Exception ex)
            {
                return new GetUserResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                UserFilter filter = new UserFilter();
                filter.Username = loginRequest.Username;
                await foreach (var user in _userRepository.RetrieveCollectionAsync(filter))
                {
                    if ( _passwordHasher.Verify(loginRequest.Password, user.UserPassword)) {
                        return new LoginResponse
                        {
                            IsSuccesful=true,
                            UserId = user.UserId
                        };
                    }
                }
                return new LoginResponse
                {
                    IsSuccesful = false,
                    Message = "Invalid username or password"
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }
            
        }

        public Task<RegisterResponse> Register(RegisterRequest registerRequest)
        {
            throw new NotImplementedException();
        }
    }
}
