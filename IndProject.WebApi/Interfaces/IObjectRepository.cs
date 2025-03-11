using IndProject.WebApi.Models;

namespace IndProject.WebApi.Interfaces
{
    public interface IObjectRepository
    {
        public Task<Object2D> InsertObject(Object2D object2D);
        public Task<IEnumerable<Object2D>> ReadObjectFromEnviroment(Guid EnviromentId);
    }
}
