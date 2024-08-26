using System.Data;

namespace ProductMarket.Libary.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
