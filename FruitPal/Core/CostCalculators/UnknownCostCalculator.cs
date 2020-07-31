using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitPal
{ 
    public class UnknownCostCalculator : CostCalculator
    {
        public UnknownCostCalculator(ILogger logger) : base(logger)
        {
        }

        public override List<string> GetCostOutputStrings(List<Commodity> commodities)
        {
            Logger.Log("Unknown commodity.");
            return new List<string>();
        }

    }
}
