using Microsoft.Data.SqlClient;
using PoolGame.Repositories.BaseClasses;
using PoolGame.Repositories.Helpers;
using PoolGame.Repositories.Interfaces.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Implementations.Game
{
    public class GameRepository : BaseRepository<Models.Game>, IGameRepository
    {
        private const string IdDbFieldEnumeratorName = "GameId";
        public Task<int> CreateAsync(Models.Game entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<Models.Game> RetrieveAsync(int objectId)
        {
           return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Game> RetrieveCollectionAsync(GameFilter filter)
        {
            Filter commandFilter = new Filter();
            if(filter.GameDate != null)
            {
                commandFilter.AddCondition("GameDate", filter.GameDate);
            }
            if (filter.GameIsDraw !=null)
            {
                commandFilter.AddCondition("GameIsDraw", filter.GameIsDraw.Value);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, GameUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                "Games",
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("GameDate", update.GameDate);
            updateCommand.AddSetClause("GameIsDraw", update.GameIsDraw);
            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        protected override string[] GetColumns()
        {
           return new string[] { "GameId", "GameDate", "GameIsDraw" };
        }

        protected override string GetTableName()
        {
            return "Games";
        }

        protected override Models.Game MapEntity(SqlDataReader reader)
        {
            return new Models.Game
            {
                GameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                GameDate = reader.GetDateTime(reader.GetOrdinal("GameDate")),
                GameIsDraw = reader.GetBoolean(reader.GetOrdinal("GameIsDraw"))

            };
        }
    }
}
