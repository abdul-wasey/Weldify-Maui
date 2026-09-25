namespace Weldify.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public IList<Job> Jobs { get; set; } = new List<Job>();
    }
}
