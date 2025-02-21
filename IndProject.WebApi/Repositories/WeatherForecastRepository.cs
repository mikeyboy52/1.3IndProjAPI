using Dapper;
using Microsoft.Data.SqlClient;

namespace IndProject.WebApi.Repositories
{
    public class WeatherForecastRepository
    {
        private readonly string sqlConnectionString;

        public WeatherForecastRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<User> InsertAsync(User user)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [User] (Id, Username, Password) VALUES (@Id, @Username, @Password)", user);
                return user;
            }
        }

        public async Task<User> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<User>("SELECT * FROM [User] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<User>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<User>("SELECT * FROM [User]");
            }
        }

        public async Task UpdateAsync(User environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [User] SET " +
                                                 "Username = @Username, " +
                                                 "Password = @Password"
                                                 , environment);

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [User] WHERE Id = @Id", new { id });
            }
        }

    }
}