using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Task.Main.Entities;

namespace Task.Main.IO
{
    /// <summary>
    /// A custom CSV file writer that is designed to write the output report
    /// </summary>
    public class PopulationChangeSummaryWriter
    {
        /// <summary>
        /// Path of the output file
        /// </summary>
        private string outputFilePath;


        /// <summary>
        /// Default constructor of the Writer that passes the hardcoded path of the output file.
        /// </summary>
        public PopulationChangeSummaryWriter() : this(Path.Combine(Constants.OutputFilePath, Constants.OutputFileName))
        {

        }

        /// <summary>
        /// Constructor that accepts the output file path to be changed for testing purposes.
        /// </summary>
        /// <param name="outputFilePath">Path of the output file that will be used to write</param>
        public PopulationChangeSummaryWriter(string outputFilePath)
        {
            if (string.IsNullOrEmpty(outputFilePath))
            {
                throw new ArgumentException("Output File Path must not be empty!");
            }
            this.outputFilePath = outputFilePath;
        }

        /// <summary>
        /// Writes the created report to the output file
        /// </summary>
        /// <param name="areaBasedSummaries">Dictionary containing area code based population change summary</param>
        public void WriteToFile(Dictionary<string, AreaPopulationChangeSummary> areaBasedSummaries)
        {
            //Make sure the output path exists or gets created
            Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
            using (StreamWriter writer = new StreamWriter(File.Open(outputFilePath, FileMode.Create)))
            {
                int keyIndexer = 0;
                string[] keys = areaBasedSummaries.Keys.ToArray();
                Array.Sort(keys);
                foreach (string areaCode in keys)
                {
                    AreaPopulationChangeSummary summary = areaBasedSummaries[areaCode];
                    List<string> lineColumns = new List<string>();
                    lineColumns.Add(summary.AreaCode);
                    lineColumns.Add(summary.AreaDescription);
                    lineColumns.Add(summary.NumberOfCensusTracts.ToString());
                    lineColumns.Add(summary.TotalPopulationIn2000.ToString());
                    lineColumns.Add(summary.TotalPopulationIn2010.ToString());
                    lineColumns.Add(summary.AveragePopulationPercentChange.ToString("0.##"));
                    string lineText = string.Join(',', lineColumns);
                    //If this is the lastIndex do not create a new line
                    if (keyIndexer == areaBasedSummaries.Keys.Count - 1)
                    {
                        writer.Write(lineText);
                    }
                    else
                    {
                        writer.WriteLine(lineText);
                    }
                    keyIndexer++;
                }
            }
        }

  
    }
}
