using Microsoft.Data.SqlClient;
using PoolGame.Repositories.BaseClasses;
using PoolGame.Repositories.Helpers;
using PoolGame.Repositories.Interfaces.PlayerStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolGame.Repositories.Implementations.PlayerStat
{
    public class PlayerStatRepository : BaseRepository<Models.PlayerStat>, IPlayerStatRepository
    {
        private const string IdDbFieldEnumeratorName = "StatId";
        public Task<int> CreateAsync(Models.PlayerStat entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
           return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<Models.PlayerStat> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.PlayerStat> RetrieveCollectionAsync(PlayerStatFilter filter)
        {
            Filter commandFilter = new Filter();
            
            if (filter.UserId is not null) {
            commandFilter.AddCondition("UserId", filter.UserId.Value);
            }
            if (filter.GameId is not null)
            {
                commandFilter.AddCondition("GameId", filter.GameId.Value);
            }
            if (filter.IsWinner is not null)
            {
                commandFilter.AddCondition("IsWinner", filter.IsWinner.Value);
            }
            if (filter.ShotsMade is not null)
            {
                commandFilter.AddCondition("ShotsMade", filter.ShotsMade.Value);
            }
            if (filter.ShotsAttempted is not null)
            {
                commandFilter.AddCondition("ShotsAttempted", filter.ShotsAttempted.Value);
            }
            if (filter.HandBalls is not null)
            {
                commandFilter.AddCondition("HandBalls", filter.HandBalls.Value);
            }
            if (filter.Fouls is not null)
            {
                commandFilter.AddCondition("Fouls", filter.Fouls.Value);
            }
            if (filter.BestStreak is not null)
            {
                commandFilter.AddCondition("BestStreak", filter.BestStreak.Value);
            }


            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, PlayerStatUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                "PlayerStats",
                IdDbFieldEnumeratorName, objectId);

            
            updateCommand.AddSetClause("IsWinner", update.IsWinner);
            updateCommand.AddSetClause("ShotsMade", update.ShotsMade);
            updateCommand.AddSetClause("ShotsAttempted", update.ShotsAttempted);
            updateCommand.AddSetClause("HandBalls", update.HandBalls);
            updateCommand.AddSetClause("Fouls", update.Fouls);
            updateCommand.AddSetClause("BestStreak", update.BestStreak);

            return await updateCommand.ExecuteNonQueryAsync() > 0;

        }

        protected override string[] GetColumns()
        {
            return new string[]
            {
                "StatId",
                "GameId",
                "UserId",
                "IsWinner",
                "ShotsMade",
                "ShotsAttempted",
                "HandBalls",
                "Fouls",
                "BestStreak"
            };
        }

        protected override string GetTableName()
        {
            return "PlayerStats";
        }

        protected override Models.PlayerStat MapEntity(SqlDataReader reader)
        {
            return new Models.PlayerStat
            {
                StatId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                GameId = Convert.ToInt32(reader["GameId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                IsWinner = Convert.ToBoolean(reader["IsWinner"]),
                ShotsMade = Convert.ToInt32(reader["ShotsMade"]),
                ShotsAttempted = Convert.ToInt32(reader["ShotsAttempted"]),
                HandBalls = Convert.ToInt32(reader["HandBalls"]),
                Fouls = Convert.ToInt32(reader["Fouls"]),
                BestStreak = Convert.ToInt32(reader["BestStreak"])
            };
        }
    }
}
