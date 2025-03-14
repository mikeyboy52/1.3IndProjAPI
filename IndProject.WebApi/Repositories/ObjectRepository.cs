﻿using Dapper;
using IndProject.WebApi.Interfaces;
using IndProject.WebApi.Models;
using Microsoft.Data.SqlClient;

namespace IndProject.WebApi.Repositories
{
    public class Object2DRepository : IObjectRepository
    {
        private readonly string sqlConnectionString;

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D> InsertObject(Object2D object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                Console.WriteLine(object2D.EnviromentId);
                var parameters = new
                {
                    Id = object2D.Id,
                    EnviromentId = object2D.EnviromentId,
                    PrefabId = object2D.PrefabId,
                    PositionX = object2D.PositionX,
                    PositionY = object2D.PositionY,
                    ScaleX = object2D.ScaleX,
                    ScaleY = object2D.ScaleY,
                    RotationZ = object2D.RotationZ,
                    SortingLayer = object2D.SortingLayer
                };
                var enviromentId = await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, EnviromentId, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer) VALUES (@Id, @EnviromentId, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)", parameters);
                return object2D;
            }
        }
        public async Task<IEnumerable<Object2D>> ReadObjectFromEnviroment(Guid EnviromentId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D] WHERE EnviromentId = @EnviromentId", new { EnviromentId });
            }
        }
    }
}