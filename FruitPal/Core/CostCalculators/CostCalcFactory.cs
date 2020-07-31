using System;

namespace FruitPal
{
    public class CostCalcFactory
    {
        private readonly ILogger _logger;

        public CostCalcFactory(ILogger logger)
        {
            _logger = logger;
        }

        public CostCalculator Create(ConsoleCommand cmd)
        {
            try
            {
                return (CostCalculator)Activator.CreateInstance(
                    Type.GetType($"FruitPal.{cmd.Name}CostCalculator", true, true),
                        new object[] { _logger, cmd.parameterValueList });
            }
            catch
            {
                return new UnknownCostCalculator(_logger);
            }
        }
    }
}
