using System.Data.Common;

namespace TodoApp.Application;
public interface IDbConnectionFactory
{
    DbConnection GetConnection();
}
