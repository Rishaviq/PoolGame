using Microsoft.Data.SqlClient;
using PoolGame.Repositories.BaseClasses;
using PoolGame.Repositories.Helpers;
using PoolGame.Repositories.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Implementations.User
{
   public class UserRepository : BaseRepository<Models.User>, IUserRepository
    {
        private const string IdDbFieldEnumeratorName = "UserId";
        public Task<int> CreateAsync(Models.User entity)
        {
          return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<Models.User> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.User> RetrieveCollectionAsync(UserFilter filter)
        {
            Filter commandFilter = new Filter();
            if (filter.Username is not null)
            {
                commandFilter.AddCondition("Username", filter.Username);
            }
            if (filter.ProfileName is not null)
            {
                commandFilter.AddCondition("ProfileName", filter.ProfileName);
            }
            if (filter.UserId is not null)
            {
                commandFilter.AddCondition("UserId", filter.UserId.Value);
            }
            return base.RetrieveCollectionAsync(commandFilter);

        }

        public async Task<bool> UpdateAsync(int objectId, UserUpdate update)
        {

            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                "Users",
                IdDbFieldEnumeratorName, objectId);
            updateCommand.AddSetClause("ProfileName", update.ProfileName);
            updateCommand.AddSetClause("UserPassword", update.UserPassword);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        protected override string[] GetColumns()
        {
            return new string[] { "UserId", "Username", "ProfileName", "UserPassword" };
        }

        protected override string GetTableName()
        {
           return "Users";
        }

        protected override Models.User MapEntity(SqlDataReader reader)
        {
            return new Models.User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                Username = Convert.ToString(reader["Username"])?? string.Empty,
                ProfileName = Convert.ToString(reader["ProfileName"]),
                UserPassword = Convert.ToString(reader["UserPassword"]) ?? string.Empty
            };
        }
    }
}
