using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Task.Main;
using Task.Main.Entities;
using Task.Main.IO;
using Task.Main.Processing;

namespace Task.Tests
{
    [TestClass]
    public class FileWriteTests : BaseTest
    {
        private string testOutputFilePath = "output/TestReport.csv";
        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testOutputFilePath))
            {
                File.Delete(testOutputFilePath);
            }
        }

        [TestMethod]
        public void EmptyResultSetTest()
        {
            PopulationChangeSummaryWriter writer = new PopulationChangeSummaryWriter(testOutputFilePath);
            Dictionary<string, AreaPopulationChangeSummary> emptySummaryDictionary = new Dictionary<string, AreaPopulationChangeSummary>();
            writer.WriteToFile(emptySummaryDictionary);
            Assert.IsTrue(File.Exists(testOutputFilePath), "No file was created for empty result set!");
        }


        [TestMethod]
        public void SimpleResultSetTest()
        {
            PopulationChangeSummaryWriter writer = new PopulationChangeSummaryWriter(testOutputFilePath);
            Dictionary<string, AreaPopulationChangeSummary> simpleSummaryDictionary = new Dictionary<string, AreaPopulationChangeSummary>();
            AreaPopulationChangeSummary summary = new AreaPopulationChangeSummary("Test", "\"A test area, Test State\"");
            summary.NumberOfCensusTracts = 2;
            summary.PopulationPercentChanges = new List<double> { 10.2, 15.1 };
            summary.TotalPopulationIn2000 = 11200;
            summary.TotalPopulationIn2010 = 13400;
            summary.UpdateAverages();
            simpleSummaryDictionary.Add(summary.AreaCode, summary);

            writer.WriteToFile(simpleSummaryDictionary);
            Assert.IsTrue(File.Exists(testOutputFilePath), "No file was created for simple result set!");
        }


        [TestMethod]
        public void SimpleResultSetWriteAndReadTest()
        {
            PopulationChangeSummaryWriter writer = new PopulationChangeSummaryWriter(testOutputFilePath);
            Dictionary<string, AreaPopulationChangeSummary> simpleSummaryDictionary = new Dictionary<string, AreaPopulationChangeSummary>();
            AreaPopulationChangeSummary summary = new AreaPopulationChangeSummary("Test", "\"A test area, Test State\"");
            summary.NumberOfCensusTracts = 2;
            summary.PopulationPercentChanges = new List<double> { 10.2, 15.1 };
            summary.TotalPopulationIn2000 = 11200;
            summary.TotalPopulationIn2010 = 13400;
            summary.UpdateAverages();
            simpleSummaryDictionary.Add(summary.AreaCode, summary);

            writer.WriteToFile(simpleSummaryDictionary);
            Assert.IsTrue(File.Exists(testOutputFilePath), "No file was created for empty result set!");

            string fileContent = File.ReadAllText(testOutputFilePath);
            string[] outputColumns = new TractPopulationChangeReader().ExtractColumnsFromLineText(fileContent, true);
            Assert.IsNotNull(outputColumns, "Written file cannot be read");
            Assert.AreEqual(6, outputColumns.Length, "Written file has invalid column count");
        }

        [TestMethod]
        public void MainDatasetTest()
        {
            new TractPopulationChangeSummarizer().SummarizeDataSet();
            Assert.IsTrue(File.Exists(Path.Combine(Constants.OutputFilePath, Constants.OutputFileName)), "No file was created for main result set!");
        }

        [TestMethod]
        public void MainDatasetValidatedTest()
        {
            string[] areaCodeFilter = new string[] { "28540", "46900" };
            new TractPopulationChangeSummarizer(areaCodeFilter).SummarizeDataSet();
            string outputFilePath = Path.Combine(Constants.OutputFilePath, Constants.OutputFileName);
            string expectedResultFilePath = "input/ExpectedResult.csv";
            Assert.IsTrue(File.Exists(outputFilePath), "No file was created for main result set!");

            string[] generatedReportLines = File.ReadAllLines(outputFilePath);
            string[] expectedResultLines = File.ReadAllLines(expectedResultFilePath);
            Assert.AreEqual(expectedResultLines.Length, generatedReportLines.Length, "Generated report and expected result do not have the same row count!");
            for (int i = 0; i < generatedReportLines.Length; i++)
            {
                Assert.AreEqual(expectedResultLines[i], generatedReportLines[i], $"Line {i + 1} do not match between expected result and generated report!");
            }
        }
    }
}
