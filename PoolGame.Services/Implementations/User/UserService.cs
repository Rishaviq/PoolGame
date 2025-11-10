using PoolGame.Models;
using PoolGame.Repositories.Interfaces.User;
using PoolGame.Services.DTOs.User;
using PoolGame.Services.DTOs.User.Requests;
using PoolGame.Services.DTOs.User.Response;
using PoolGame.Services.DTOs.User.Responses;
using PoolGame.Services.Exceptions;
using PoolGame.Services.Helpers;
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
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
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


                Models.User user = await FindUserByName(loginRequest.Username);
                if (_passwordHasher.Verify(loginRequest.Password, user.UserPassword))
                {
                    return new LoginResponse
                    {
                        IsSuccesful = true,
                        UserId = user.UserId,
                        AuthToken = _tokenProvider.CreateToken(user),
                        ProfileName = user.ProfileName ?? string.Empty
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

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            Validation validations = await RegisterValidaton(request);
            if (validations.IsValid)
            {
                await _userRepository.CreateAsync(new Models.User
                {
                    Username = request.Username,
                    ProfileName = request.ProfileName,
                    UserPassword = _passwordHasher.Hash(request.UserPassword)
                });
                return new RegisterResponse();
                
            }
            else
            {
                return new RegisterResponse
                {
                    IsSuccesful = false,
                    Message = validations.ErrorMessage ?? "Error in the registration process"
                };
            }
        }



        public async Task<Models.User> FindUserByName(string Username)
        {
            UserFilter filter = new UserFilter();
            filter.Username = Username;
            int recordCount = 0;
            Models.User? response = null;
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

        public async Task<Validation> ValidateUsername(string username)
        {
            UserFilter filter = new UserFilter();
            filter.Username = username;


            await foreach (var user in _userRepository.RetrieveCollectionAsync(filter))
            {

                return new Validation("Username already exist");
            }
            Validation lenghtValidation = GenetalStringValidation(username);
            if (!lenghtValidation.IsValid)
            {

                lenghtValidation.ErrorMessage = lenghtValidation.ErrorMessage?.Replace("String", "Username") ?? "Error in the username lenght";
                return lenghtValidation;
            }


            return new Validation();
        }
        public Validation GenetalStringValidation(string input)
        {
            if (input.Length < 3 || input.Length > 50)
            {
                return new Validation("String should be between 3 and 50 characters");
            }
            return new Validation();
        }

        public async Task<Validation> RegisterValidaton(RegisterRequest request)
        {
            Validation response = new Validation();
            response = await ValidateUsername(request.Username);
            if (!response.IsValid)
            {
                return response;
            }
            response = GenetalStringValidation(request.UserPassword);
            if (!response.IsValid) { return response; }

            if (request.ProfileName is not null)
            {
                response = GenetalStringValidation(request.ProfileName);
                if (!response.IsValid) { return response; }
            }


            return new Validation();
        }
    }
}
