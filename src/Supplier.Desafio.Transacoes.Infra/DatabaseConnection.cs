
using MySql.Data.MySqlClient;
using System.Data;

namespace Supplier.Desafio.Clientes.Infra
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
