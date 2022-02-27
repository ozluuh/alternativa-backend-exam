namespace Domain.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Value { get; set; }
        public string Brand { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
