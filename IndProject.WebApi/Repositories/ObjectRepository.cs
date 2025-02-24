using Dapper;
using IndProject.WebApi.Models;
using Microsoft.Data.SqlClient;

namespace IndProject.WebApi.Repositories
{
    public class ObjectRepository
    {
        private readonly string sqlConnectionString;

        public ObjectRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D> InsertObject(Object2D object2d)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var enviromentId = await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer) VALUES (@Id, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)", object2d);
                return object2d;
            }
        }

        public async Task<Object2D> ReadObject(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>("SELECT * FROM [Object2D] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Object2D>> ReadObjects()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D]");
            }
        }

        public async Task UpdateObject(Object2D object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Enviroment2D] SET " +
                                                 "PositionX = @PositionX, " +
                                                 "PositionY = @PositionY, " +
                                                 "ScaleX = @ScaleX, " + 
                                                 "ScaleY = @ScaleY, " + 
                                                 "RotationZ = @RotationZ, " +
                                                 "SortingLayer = @SortingLayer"
                                                 , object2D);

            }
        }

        public async Task DeleteObject(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Enviroment2D] WHERE Id = @Id", new { id });
            }
        }

    }
}