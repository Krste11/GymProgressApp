using Gym.Progres.Domain;
using Gym.Progres.Services.Implementations;
using GymProgress.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace GymProgress.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExcersiceService _exerciseService;

        public ExerciseController(IExcersiceService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var exercises = await _exerciseService.GetAllAsync();
            return Ok(exercises);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Exercise exercise)
        {
            var newExercise = await _exerciseService.AddAsync(exercise);
            return CreatedAtAction(nameof(GetAll), new { id = newExercise.Id }, newExercise);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _exerciseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
