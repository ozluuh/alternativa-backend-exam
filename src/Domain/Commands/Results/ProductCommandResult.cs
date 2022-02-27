using System.Text.Json.Serialization;

namespace Domain.Commands.Results
{
    public class ProductCommandResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string Brand { get; set; }

        // NOTE: change property name according api documentation samples
        [JsonPropertyName("categoriaid")]
        public long CategoryId { get; set; }

        [JsonPropertyName("categoria")]
        public CategoryCommandResult Category { get; set; }
    }
}
