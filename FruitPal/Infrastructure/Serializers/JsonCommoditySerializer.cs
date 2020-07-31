using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace FruitPal
{
    public class JsonCommoditySerializer : ICommoditySerializer
    {
        public List<Commodity> GetCommodityDataFromString(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<Commodity>>(jsonString);
        }
    }
}
