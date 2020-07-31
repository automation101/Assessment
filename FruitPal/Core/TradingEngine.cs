using System;
using System.Collections.Generic;
using System.Linq;


namespace FruitPal
{
    /// <summary>
    /// The Trading takes the commodity input by the trader and produces 
    /// a list of full cost information for buying the fruit based on data 
    /// read from a file sent by a 3rd Party.
    /// </summary>
    public class TradingEngine
    {
        private readonly ILogger _logger;
        private readonly ConsoleCommand _command;
        private readonly ICommoditySource _commoditySource;
        private readonly ICommoditySerializer _commoditySerializer;
        private readonly CostCalcFactory _costCalcFactory;

        public List<string> CommodityCosts { get; set; }

        public TradingEngine(ILogger logger,
            ConsoleCommand command,
            ICommoditySource commoditySource,
            ICommoditySerializer commoditySerializer,
            CostCalcFactory costCalcFactory)
        {
            _logger = logger;
            _command = command;
            _commoditySource = commoditySource;
            _commoditySerializer = commoditySerializer;
            _costCalcFactory = costCalcFactory;
        }

        public void GetCostOutput()
        {
            _logger.Log("Starting cost logic.");
            //load data from 3rd Party - for now we're reading directly from JSON file, 
            //but this could be changed in the future to have data loaded into database with a nightly process and then read from DB. 
            //In that case, I'd do the filtering, calc & sorting in SQL instead of LINQ 
            _logger.Log("Loading data.");
            string commodityJson = _commoditySource.GetCommodityFromSource();

            var commodityList = _commoditySerializer.GetCommodityDataFromString(commodityJson);

            var costCalc = _costCalcFactory.Create(_command);

            CommodityCosts = costCalc.GetCostOutputStrings(commodityList);

            _logger.Log("Cost logic completed.");
         
        }
    }
}
