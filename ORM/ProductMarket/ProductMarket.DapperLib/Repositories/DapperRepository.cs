using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace ProductMarket.DapperLib.Repositories
{
    public class DapperRepository<TResult> : IDapperRepository<TResult> where TResult : class
    {
        private readonly IDbConnection connection;

        public DapperRepository(SqlConnection sqlConnection)
        {
            connection = sqlConnection;
        }

        public async Task DeleteAsync(string query, DynamicParameters parametrs = null,
                                            CommandType type = CommandType.Text)
        {
            await connection.QueryAsync(query, param: parametrs, commandType: type);
        }

        public async Task<List<TResult>> SelectAllAsync(string query, DynamicParameters parametrs = null,
                                            CommandType commandType = CommandType.Text)
        {
            return (await connection.QueryAsync<TResult>(query, param: parametrs, commandType: commandType)).ToList();
        }

        public async Task<TResult> SelectAsync(string query, DynamicParameters parametrs = null,
                                            CommandType commandType = CommandType.Text)
        {
            return await connection.QueryFirstOrDefaultAsync<TResult>(query, param: parametrs, commandType: commandType);
        }

        public async Task UpdateAsync(string query, DynamicParameters parametrs = null,
                                            CommandType commandType = CommandType.Text)
        {
            await connection.QueryAsync<TResult>(query, param: parametrs, commandType: commandType);
        }

        public async Task<int> ExecuteInsertAsync(string query, DynamicParameters parametrs = null,
                                                  CommandType commandType = CommandType.Text)
        {
            return await connection.ExecuteScalarAsync<int>(query, param: parametrs, commandType: commandType);
        }
    }
}
