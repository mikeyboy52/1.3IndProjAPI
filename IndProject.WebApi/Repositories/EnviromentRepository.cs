using Dapper;
using IndProject.WebApi.Models;
using Microsoft.Data.SqlClient;

namespace IndProject.WebApi.Repositories
{
    public class EnviromentRepository
    {
        private readonly string sqlConnectionString;

        public EnviromentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Enviroment> InsertEnviroment(Enviroment enviroment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var parameters = new
                {
                    Id = enviroment.Id,
                    Name = enviroment.Name,
                    Email = enviroment.Email,
                    MaxHeight = enviroment.MaxHeight,
                    MaxLength = enviroment.MaxLength
                };
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [Enviroment2D] (Id, Name, Email, MaxHeight, MaxLength) VALUES (@Id, @Name, @Email, @MaxHeight, @MaxLength)", parameters);
                return enviroment;
            }
        }

        public async Task<Enviroment> ReadEnviroment(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Enviroment>("SELECT * FROM [Enviroment2D] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Enviroment>> ReadEnviroments()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Enviroment>("SELECT * FROM [Enviroment2D]");
            }
        }

        public async Task UpdateEnviroment(Enviroment environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Enviroment2D] SET " +
                                                 "Name = @Name, " +
                                                 "MaxHeight = @MaxHeight, " +
                                                 "MaxLength = @MaxLength"
                                                 , environment);

            }
        }

        public async Task DeleteEnviroment(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Enviroment2D] WHERE Id = @Id", new { id });
            }
        }

    }
}