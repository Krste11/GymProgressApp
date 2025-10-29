namespace Gym.Progres.Domain
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Repetitions { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

    }
}
