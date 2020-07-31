using System.IO;

namespace FruitPal
{
    public class FileCommoditySource : ICommoditySource
    {
        public string GetCommodityFromSource()
        {
            return File.ReadAllText("commoditydata.json");
        }
    }
}
