using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FruitPal
{
    public abstract class CostCalculator
    {
        public ILogger Logger { get; set; }
        public string CommodityName { get; set; }
        public decimal PricePerTon { get; set; }
        public decimal TradeVolume { get; set; }

        public CostCalculator(ILogger logger)
        {
            Logger = logger;
        }

        public abstract List<string> GetCostOutputStrings(List<Commodity> commodities);

        public virtual decimal CalculateTotalCost(decimal fixedOverhead, decimal varOverhead)
        {
            decimal totalCost = 0;
            totalCost = TradeVolume * (PricePerTon + varOverhead) + fixedOverhead;
            return totalCost;
        }

        public virtual string FormatOutputCostString(Commodity item)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("< ");
            sb.Append(item.Country);
            sb.Append(" ");
            sb.Append(item.TotalCost);
            sb.Append(" | ");
            sb.Append("(");
            sb.Append(item.VariableOverhead + PricePerTon);
            sb.Append("*");
            sb.Append(TradeVolume.ToString().Replace(".00", ""));
            sb.Append(")");
            sb.Append("+");
            sb.Append(item.FixedOverhead.ToString().Replace(".00", ""));

            return sb.ToString();
        }
    }
}
