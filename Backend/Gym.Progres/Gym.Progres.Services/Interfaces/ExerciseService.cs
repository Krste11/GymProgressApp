using Gym.Progres.DataAccess;
using Gym.Progres.Domain;
using Gym.Progres.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace GymProgress.Services.Implementations
{
    public class ExerciseService : IExcersiceService
    {
        private readonly AppDbContext _context;

        public ExerciseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(int id)
        {
            return await _context.Exercises.FindAsync(id);
        }

        public async Task<Exercise> AddAsync(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();
            return exercise;
        }

        public async Task DeleteAsync(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
            }
        }
    }
}
