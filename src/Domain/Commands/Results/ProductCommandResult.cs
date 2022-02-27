namespace Domain.Commands.Results
{
    public class ProductCommandResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string Brand { get; set; }
        public long CategoryId { get; set; }
        public CategoryCommandResult Category { get; set; }
    }
}
