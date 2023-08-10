using DataLibrary;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;

namespace ToDoList.Data.DataAccessObjects.MSSQL
{
    public class UserDao : IUserDao
    {
        private readonly IConfiguration _configuration;

        public UserDao(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public User Login(string username, string password)
        {
            User user = null;
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "SELECT id, username " +
                "FROM users " +
                "WHERE username = @username AND password = @password";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@password", password);

                user = conn.QueryFirstOrDefault<User>(sql, parameters);
            }
            return user;
        }

        public User Register(User user, string password)
        {
            using (IDbConnection conn = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                string sql = "INSERT INTO Users(username, password) " +
                    "OUTPUT INSERTED.id " +
                    "VALUES (@username, @password)";
                var parameters = new DynamicParameters();
                parameters.Add("@username", user.Username);
                parameters.Add("@password", password);

                user.Id = conn.ExecuteScalar<int>(sql, parameters);
            }
            return user;
        }
    }
}
