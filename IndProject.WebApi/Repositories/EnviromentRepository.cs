using Dapper;
using IndProject.WebApi.Interfaces;
using IndProject.WebApi.Models;
using Microsoft.Data.SqlClient;

namespace IndProject.WebApi.Repositories
{
    public class EnviromentRepository : IEnviromentRepository
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

        public async Task<IEnumerable<Enviroment>> ReadEnviroment(string email)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Enviroment>("SELECT * FROM [Enviroment2D] WHERE Email = @Email", new { email });
            }
        }
        public async Task<Enviroment> ReadEnviroment(string email, string name)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var parameters = new
                {
                    Email = email,
                    Name = name
                };
                return await sqlConnection.QuerySingleOrDefaultAsync<Enviroment>("SELECT * FROM [Enviroment2D] WHERE Email = @Email AND Name = @Name", parameters);
            }
        }

        public async Task<Enviroment> ReadEnviroment(Guid Id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Enviroment>("SELECT * FROM [Enviroment2D] WHERE Id = @Id", new { Id });
            }
        }

        public async Task UpdateEnviroment(Enviroment enviroment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var parameters = new
                {
                    Id = enviroment.Id,
                    Name = enviroment.Name,
                    MaxHeight = enviroment.MaxHeight,
                    MaxLength = enviroment.MaxLength
                };
                await sqlConnection.ExecuteAsync("UPDATE [Enviroment2D] SET " +
                                                 "Name = @Name, " +
                                                 "MaxHeight = @MaxHeight, " +
                                                 "MaxLength = @MaxLength " +
                                                 "WHERE Id = @Id"
                                                 , enviroment);

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