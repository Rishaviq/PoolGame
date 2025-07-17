
using PoolGame.Repositories.Implementations.Game;
using PoolGame.Repositories.Implementations.PlayerStat;
using PoolGame.Repositories.Implementations.User;
using PoolGame.Repositories.Interfaces.Game;
using PoolGame.Repositories.Interfaces.PlayerStat;
using PoolGame.Repositories.Interfaces.User;
using PoolGame.Services.Interfaces.Game;
using PoolGame.Services.Interfaces.PlayerStat;
using PoolGame.Services.Interfaces.User;
using PoolGame.Services.Implementations.Game;
using PoolGame.Services.Implementations.PlayerStat;
using PoolGame.Services.Implementations.User;
using PoolGame.Services.Helpers.Interfaces;
using PoolGame.Services.Helpers.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PoolGameAPI.Controllers;

namespace PoolGame.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //adding repositories 
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPlayerStatRepository, PlayerStatRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();


            //adding services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPlayerStatService, PlayerStatService>();
            builder.Services.AddScoped<IGameService, GameService>();


            //adding helpers
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddSingleton<ITokenProvider, TokenProvider>();


            if(builder.Environment.IsDevelopment())
{
                builder.Configuration.AddUserSecrets<Program>(optional: true, reloadOnChange: true);
            }

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero

                    };

                });

           



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
