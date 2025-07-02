using PoolGame.Models;
using PoolGame.Repositories.Interfaces.User;
using PoolGame.Services.DTOs.User;
using PoolGame.Services.DTOs.User.Request;
using PoolGame.Services.DTOs.User.Requests;
using PoolGame.Services.DTOs.User.Response;
using PoolGame.Services.DTOs.User.Responses;
using PoolGame.Services.Exceptions;
using PoolGame.Services.Helpers.Interfaces;
using PoolGame.Services.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Services.Implementations.User
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly ITokenProvider _tokenProvider;
        public UserService(IUserRepository userRepository,IPasswordHasher passwordHasher,ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
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
                
                
                Models.User user= await FindUserByName(loginRequest.Username);
                if (_passwordHasher.Verify(loginRequest.Password, user.UserPassword))
                {
                    return new LoginResponse
                    {
                        IsSuccesful = true,
                        UserId = user.UserId,
                        AwthToken = _tokenProvider.CreateToken(user)
                    };
                }
                return new LoginResponse
                {
                    IsSuccesful = false,
                    Message = "Invalid username or password"
                };
            }
            catch (UserValidationException ex)
            { 
                Console.WriteLine(ex.Message);
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

        public async Task<Models.User> FindUserByName(string Username) {
            UserFilter filter = new UserFilter();
            filter.Username = Username;
            int recordCount = 0;
            Models.User? response =  null;
            await foreach (var user in _userRepository.RetrieveCollectionAsync(filter))
            {
                recordCount++;
                response = user;
            }
            if (recordCount == 0)
            {
                throw new UserValidationException("User not found");
            }
            else if (recordCount > 1)
            {
                throw new UserValidationException($"Multiple users found with the same username - {Username}");
            }
            if (response == null)
            {
                throw new UserValidationException("Server error - user is somehow null");
            }
            return response;
        }
    }
}
