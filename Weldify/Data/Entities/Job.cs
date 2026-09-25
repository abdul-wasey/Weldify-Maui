using Weldify.Enums;

namespace Weldify.Data.Entities
{
    public class Job
    {
        public int Id { get; set; }

        public string JobNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public Customer Customer { get; set; } = null!;

        public JobStatus Status { get; set; } = JobStatus.Draft;

        public bool InstallationRequired { get; set; }

        public string? SiteAddress { get; set; }

        public FinishType Finish { get; set; } = FinishType.None;

        public string? Color { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public IList<JobItem> Items { get; set; } = new List<JobItem>();

        public IList<JobImage> Images { get; set; } = new List<JobImage>();
    }
}
