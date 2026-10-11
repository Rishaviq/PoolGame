using Microsoft.EntityFrameworkCore;
using PoolGame.EFCore.Data;
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
        private readonly ITokenProvider _tokenProvider;
        private readonly PoolGameDbContext _dbContext;
        public UserService(PoolGameDbContext dbContext, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
        }
        public async Task<GetUserResponse> GetUser(int userId)
        {
            try
            {
                EFCore.Models.User? user = await _dbContext.Users.FindAsync(userId);


                if (user == null) return new GetUserResponse { Message = "This user doesnt exist", IsSuccesful = false };


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


                var user = await _dbContext.Users.Where(x => x.Username == loginRequest.Username).SingleOrDefaultAsync();
                
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
                 _dbContext.Users.Add(new EFCore.Models.User
                {
                    Username = request.Username,
                    ProfileName = request.ProfileName,
                    UserPassword = _passwordHasher.Hash(request.UserPassword)
                });
                await _dbContext.SaveChangesAsync();
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



        public async Task<EFCore.Models.User> FindUserByName(string Username)
        {
            var response = await _dbContext.Users.Where(x => x.Username == Username).SingleOrDefaultAsync();
            if (response == null) {
                throw new UserValidationException("No such user exists");
            }
            return response;
        }

        public async Task<Validation> ValidateUsername(string username)
        {

            var user = await _dbContext.Users.Where(x => x.Username == username).FirstOrDefaultAsync();

            if(user is not null) return new Validation("Username already exist");
            


            Validation lenghtValidation = GeneralStringValidation(username);
            if (!lenghtValidation.IsValid)
            {

                lenghtValidation.ErrorMessage = lenghtValidation.ErrorMessage?.Replace("String", "Username") ?? "Error in the username lenght";
                return lenghtValidation;
            }


            return new Validation();
        }
        public Validation GeneralStringValidation(string input)
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
            response = GeneralStringValidation(request.UserPassword);
            if (!response.IsValid) { return response; }

            if (request.ProfileName is not null)
            {
                response = GeneralStringValidation(request.ProfileName);
                if (!response.IsValid) { return response; }
            }


            return new Validation();
        }
    }
}
