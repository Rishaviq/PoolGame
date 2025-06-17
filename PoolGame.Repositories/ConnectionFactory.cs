using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace PoolGame.Repositories
{

    public class ConnectionFactory
    {
        private static string? _connectionString;

        public static void Initialize(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static async Task<SqlConnection> CreateConnectionAsync()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Task.FromException<SqlConnection>(
                    new InvalidOperationException("Failed to create a database connection.", ex)).Result;
            }
        }
    }

}
