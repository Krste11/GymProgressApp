using Gym.Progres.Domain;

namespace Gym.Progres.Services.Implementations
{
    public interface IExcersiceService
    {
        Task<IEnumerable<Exercise>> GetAllAsync();
        Task<Exercise?> GetByIdAsync(int id);
        Task<Exercise> AddAsync(Exercise exercise);
        Task DeleteAsync(int id);
    }
}
