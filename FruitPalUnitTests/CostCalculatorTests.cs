using FruitPal;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace FruitPalTests
{
    [TestFixture]
    public class TestCostCalculator
    {
        //Parameterized test using the TestCase attribute of Nunit

        //Passing different input parameter to test cost same method will run 3 times against each testcase input 
        [TestCase("orange",20, 10, 10, 10, 310)]
        [TestCase("pinapple",25.25, 10, 10, 10, 362.5)]
        [TestCase("mango",-10, 10, 10, 10, 10)]
        [Category("pass")]
        public void TestCalculateTotalCost(string commodityName, decimal tradeVolume, 
            decimal pricePerTon, decimal varOverhead,decimal fixedOverhead, decimal expOutput)
        {
            FruitPalCostCalculator calc = new FruitPalCostCalculator(null,new List<object>() { commodityName, tradeVolume, pricePerTon});

            //assert that cost calulation against input parameter 
            Assert.AreEqual(expOutput, calc.CalculateTotalCost(varOverhead, fixedOverhead));
        }


        // Testcases for coststring function to test commodity format 
        [TestCase("< US 0 | (0.55*0)+50", "{\"COUNTRY\":\"US\",\"COMMODITY\":\"pineapple\"," +
            "\"FIXED_OVERHEAD\":\"50.00\",\"VARIABLE_OVERHEAD\":\"0.55\"}")]


        [TestCase("< MX 0 | (1.24*0)+32", "{\"COUNTRY\":\"MX\",\"COMMODITY\":\"mango\"," +
            "\"FIXED_OVERHEAD\":\"32.00\",\"VARIABLE_OVERHEAD\":\"1.24\"}")]

        public void TestFormatOutputCostString(string expOutput, string inCommodity)
        {
            Commodity commodity = JsonConvert.DeserializeObject<Commodity>(inCommodity);

            FruitPalCostCalculator calc = new FruitPalCostCalculator(null, new List<object>() { commodity.Name, 0M, 0M });
            Assert.AreEqual(expOutput, calc.FormatOutputCostString(commodity));
        }

        //Negative testcase 
        [TestCase]
        public void TestFormatOutputCostStringWithNullCommodity()
        {
            try
            {
                Commodity commodity = null;

                FruitPalCostCalculator calc = new FruitPalCostCalculator(null, new List<object>() { "Orange", 0M, 0M });
                calc.FormatOutputCostString(commodity);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestCase]
        public void TestFormatOutputCostStringWithUninitializedCommodity()
        {
            try
            {
                Commodity commodity = new Commodity();

                FruitPalCostCalculator calc = new FruitPalCostCalculator(null, new List<object>() { "Orange", 0M, 0M });
                calc.FormatOutputCostString(commodity);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }

        [TestCase]
        public void TestGetCostOutputStrings()
        {
            string commodityJson = "[{\"COUNTRY\":\"MX\",\"COMMODITY\":\"mango\",\"FIXED_OVERHEAD\":\"32.00\",\"VARIABLE_OVERHEAD\":\"1.24\"},{\"COUNTRY\":\"BR\",\"COMMODITY\":\"mango\",\"FIXED_OVERHEAD\":\"20.00\",\"VARIABLE_OVERHEAD\":\"1.42\"},{\"COUNTRY\":\"US\",\"COMMODITY\":\"pineapple\",\"FIXED_OVERHEAD\":\"50.00\",\"VARIABLE_OVERHEAD\":\"0.55\"},{\"COUNTRY\":\"US\",\"COMMODITY\":\"orange\",\"FIXED_OVERHEAD\":\"22.00\",\"VARIABLE_OVERHEAD\":\"2.01\"},{\"COUNTRY\":\"MX\",\"COMMODITY\":\"pineapple\",\"FIXED_OVERHEAD\":\"32.00\",\"VARIABLE_OVERHEAD\":\"1.59\"}]";
            List<Commodity> commodityCollection = JsonConvert.DeserializeObject<List<Commodity>>(commodityJson);

            FruitPalCostCalculator calc = new FruitPalCostCalculator(new FileLogger(), new List<object>() { "Orange", 10M, 10M });
            var result = calc.GetCostOutputStrings(commodityCollection);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual("< US 142.10 | (12.01*10)+22", result[0]);



        }
    }
}