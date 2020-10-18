using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Task.Main.Entities;
using Task.Main.IO;
using Task.Main.Processing;

namespace Task.Tests
{
    [TestClass]
    public class ProcessingTests : BaseTest
    {
        [TestMethod]
        public void BasicLineProcessingTest()
        {
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader())
            {
                TractPopulationChangeProccessor proccessor = new TractPopulationChangeProccessor();
                string testLine = "01005950100,01,005,950100,187.3614167,11.21928403,,21640,\"Eufaula, AL - GA\",,2,C,3848,1735,3321,1627,-527,-13.70,-108,-6.22";
                string[] parsedColumns = reader.ExtractColumnsFromLineText(testLine);
                PopulationChangeDatum populationChange = proccessor.ParseTextToDatum(parsedColumns);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void LineProcessingInvalidDataTest()
        {
            string invalidDataFilePath = "input/InvalidColumnValueData.csv";
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader(invalidDataFilePath))
            {
                TractPopulationChangeProccessor proccessor = new TractPopulationChangeProccessor();
                reader.OpenAndValidateFile();
                PopulationChangeDatum populationChange;
                while ((populationChange = reader.Read(proccessor.ParseTextToDatum)) != null) ;
            }
        }

        [TestMethod]
        public void LineProcessingWithQuotedValuesTest()
        {
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader())
            {
                TractPopulationChangeProccessor proccessor = new TractPopulationChangeProccessor();
                string testLine = "02185000300,02,185,000300,3171.90115,849.3578553,,,,,3,,8,2,2527,1,2519,\"31,487.50\",-1,-50.00";
                string[] parsedColumns = reader.ExtractColumnsFromLineText(testLine);
                PopulationChangeDatum populationChange = proccessor.ParseTextToDatum(parsedColumns);
            }
        }
    }
}
