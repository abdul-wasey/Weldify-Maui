using Weldify.Enums;

namespace Weldify.Data.Entities
{
    public class JobItem
    {
        public int Id { get; set; }

        public int JobId { get; set; }

        public Job Job { get; set; } = null!;

        public ProductType ProductType { get; set; }

        public MaterialType MaterialType { get; set; }

        public IronGauge Gauge { get; set; }

        public string? MaterialSize { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public decimal Length { get; set; }

        public MeasurementUnit Unit { get; set; } = MeasurementUnit.Feet;

        public int Quantity { get; set; } = 1;

        public string? DesignNotes { get; set; }
    }
}
