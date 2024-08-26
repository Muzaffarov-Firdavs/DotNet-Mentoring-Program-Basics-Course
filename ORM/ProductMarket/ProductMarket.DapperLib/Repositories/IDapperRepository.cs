using Dapper;
using System.Data;

namespace ProductMarket.DapperLib.Repositories
{
    public interface IDapperRepository<TResult> where TResult : class
    {
        Task UpdateAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task DeleteAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task<TResult> SelectAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task<List<TResult>> SelectAllAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
        Task<int> ExecuteInsertAsync(string query, DynamicParameters parametrs = null,
                                CommandType commandType = CommandType.Text);
    }
}
