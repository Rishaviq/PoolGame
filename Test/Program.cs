using PoolGame.Repositories;
using PoolGame.Repositories.Implementations.Game;
using PoolGame.Repositories.Implementations.PlayerStat;
using PoolGame.Repositories.Implementations.User;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConnectionFactory.Initialize("Server=localhost;database=PoolGameWebAPP;TrustServerCertificate=True;Trusted_Connection = True");
            UserRepository userRepository = new UserRepository();
            await foreach (var a in userRepository.RetrieveCollectionAsync(new PoolGame.Repositories.Interfaces.User.UserFilter())) {
                Console.WriteLine(a);
            }
            
            PlayerStatRepository playerStatRepository = new PlayerStatRepository();
            await foreach (var b in playerStatRepository.RetrieveCollectionAsync(new PoolGame.Repositories.Interfaces.PlayerStat.PlayerStatFilter())) {

                Console.WriteLine(b);
            }

            GameRepository gameRepository = new GameRepository();
            await foreach (var c in gameRepository.RetrieveCollectionAsync(new PoolGame.Repositories.Interfaces.Game.GameFilter())) {
                Console.WriteLine(c);
            }
        }
    }
}
