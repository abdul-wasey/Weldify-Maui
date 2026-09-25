namespace Weldify.Data.Entities
{
    public class JobImage
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; } = null!;

        public string FilePath { get; set; } = string.Empty;

        public string? Caption { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
