using Dapper;
using IndProject.WebApi.Models;
using Microsoft.Data.SqlClient;

namespace IndProject.WebApi.Repositories
{
    public class UserRepository
    {
        private readonly string sqlConnectionString;

        public UserRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<User> InsertUser(User user)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [User] (Username, Password) VALUES (@Username, @Password)", user);
                return user;
            }
        }

        public async Task<User> ReadUser(string Username)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [User] WHERE Username = @Username", new { Username });
            }
        }

        public async Task<IEnumerable<User>> ReadUsers()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<User>("SELECT * FROM [User]");
            }
        }

        public async Task UpdateUser(User environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [User] SET " +
                                                 "Username = @Username, " +
                                                 "Password = @Password"
                                                 , environment);

            }
        }

        public async Task DeleteUser(string Username)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [User] WHERE Username = @Username", new { Username });
            }
        }

    }
}