using System.Collections.Generic;

namespace FruitPal
{
    public interface ICommoditySerializer
    {
        List<Commodity> GetCommodityDataFromString(string commodityString);
    }
}
