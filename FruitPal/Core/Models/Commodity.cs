using Newtonsoft.Json;

namespace FruitPal
{
    public class Commodity
    {
        public string Country { get; set; }
        [JsonProperty("Commodity")]
        public string Name { get; set; }
        [JsonProperty("Fixed_Overhead")]
        public decimal FixedOverhead { get; set; }
        [JsonProperty("Variable_Overhead")]
        public decimal VariableOverhead { get; set; }
        [JsonIgnore]
        public decimal TotalCost { get; set; }
        [JsonIgnore]
        public string OutputCostString { get; set; }
    }
}
