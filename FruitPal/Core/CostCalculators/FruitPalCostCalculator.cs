using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FruitPal
{
    public class FruitPalCostCalculator : CostCalculator
    {
        public FruitPalCostCalculator(ILogger logger, List<object> parameters) : base(logger)
        {
            CommodityName = ((string)parameters[0]).ToLower().Trim();
            PricePerTon = (decimal)parameters[1];
            TradeVolume = (decimal)parameters[2];
        }

        public override List<string> GetCostOutputStrings(List<Commodity> fruits)
        {
            Logger.Log("Filtering fruit results by input fruit.");
            var fruitsFiltered = fruits.Where(x => x.Name.ToLower().Trim() == CommodityName).ToList();

            Logger.Log("Calculating total cost and formatting output string.");
            foreach (var f in fruitsFiltered)
            {
                f.TotalCost = CalculateTotalCost(f.FixedOverhead, f.VariableOverhead);
                f.OutputCostString = FormatOutputCostString(f);
            }

            return fruitsFiltered.OrderByDescending(x => x.TotalCost).Select(o => o.OutputCostString).ToList();
        }
    }
}
