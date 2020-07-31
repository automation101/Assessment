using System;

namespace FruitPal
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "FruitPal";
            Run();
        }

        static void Run()
        {
            while (true)
            {
                Console.Write("> ");
                var consoleInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(consoleInput)) continue;

                try
                {
                    var logger = new FileLogger();

                    var cmd = new ConsoleCommand(consoleInput);

                    TradingEngine engine = new TradingEngine(logger, cmd,
                            new FileCommoditySource(),
                            new JsonCommoditySerializer(),
                            new CostCalcFactory(logger));

                    engine.GetCostOutput();

                    if (engine.CommodityCosts.Count > 0)
                    {
                        foreach (var cost in engine.CommodityCosts) {
                            Console.WriteLine(cost);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results for the input commodity found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        } 
    }
}
