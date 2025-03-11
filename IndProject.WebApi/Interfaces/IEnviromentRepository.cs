using IndProject.WebApi.Models;

namespace IndProject.WebApi.Interfaces
{
    public interface IEnviromentRepository
    {
        public Task<Enviroment> InsertEnviroment(Enviroment enviroment);
        public Task<IEnumerable<Enviroment>> ReadEnviroment(string email);
        public Task<Enviroment> ReadEnviroment(string email, string name);
        public Task<Enviroment> ReadEnviroment(Guid Id);
        public Task UpdateEnviroment(Enviroment enviroment);
        public Task DeleteEnviroment(Guid id);
    }
}
