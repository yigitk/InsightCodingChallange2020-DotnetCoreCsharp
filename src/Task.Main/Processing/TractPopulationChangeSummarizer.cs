using System.Collections.Generic;
using System.Linq;
using Task.Main.Entities;
using Task.Main.IO;

namespace Task.Main.Processing
{
    /// <summary>
    /// Class responsible for orchestrating the complete flow of summarization of the dataset
    /// </summary>
    public class TractPopulationChangeSummarizer
    {
        private TractPopulationChangeReader reader;
        private TractPopulationChangeProccessor proccessor;
        private Dictionary<string, AreaPopulationChangeSummary> AreaBasedPopulationChanges = new Dictionary<string, AreaPopulationChangeSummary>();

        private string[] areaCodeFilter;

        /// <summary>
        /// Default constructor of the summarizer that works on the whole dataset
        /// </summary>
        public TractPopulationChangeSummarizer()
        {

        }

        /// <summary>
        /// Area code filtered constructor of the summarizer that only works on the specified areas' dataset
        /// </summary>
        /// <param name="areaCode"></param>
        public TractPopulationChangeSummarizer(string[] areaCodes)
        {
            this.areaCodeFilter = areaCodes;
        }

        /// <summary>
        /// Executes the flow of parsing the input file, summarizing the dataset for the required output
        /// and finally saving the result in the output file
        /// </summary>
        public void SummarizeDataSet()
        {
            using (reader = new TractPopulationChangeReader())
            {
                AreaBasedPopulationChanges = new Dictionary<string, AreaPopulationChangeSummary>();
                proccessor = new TractPopulationChangeProccessor();
                reader.OpenAndValidateFile();
                PopulationChangeDatum populationChange;
                //Read new lines and parse them until the EOF is reached
                while ((populationChange = reader.Read(proccessor.ParseTextToDatum)) != null)
                {
                    if (!string.IsNullOrEmpty(populationChange.CoreBasedAreaCode) &&
                        (areaCodeFilter == null || areaCodeFilter.Contains(populationChange.CoreBasedAreaCode)))
                    {
                        ProcessDatum(populationChange);
                    }
                }
                PopulationChangeSummaryWriter writer = new PopulationChangeSummaryWriter();
                writer.WriteToFile(AreaBasedPopulationChanges);
            }
        }

        /// <summary>
        /// Processes a single record that was fetched from the input file and adds in to the overall summary
        /// </summary>
        /// <param name="populationChange">A single record that was fetched from the input file</param>
        private void ProcessDatum(PopulationChangeDatum populationChange)
        {
            bool alreadyExists = false;
            AreaPopulationChangeSummary summary;
            if (AreaBasedPopulationChanges.ContainsKey(populationChange.CoreBasedAreaCode))
            {
                alreadyExists = true;
                summary = AreaBasedPopulationChanges[populationChange.CoreBasedAreaCode];
            }
            else
            {
                summary = new AreaPopulationChangeSummary(populationChange.CoreBasedAreaCode, populationChange.CoreBasedAreaTitle);
            }

            summary.TotalPopulationIn2000 += populationChange.TotalPopulationIn2000;
            summary.TotalPopulationIn2010 += populationChange.TotalPopulationIn2010;
            summary.NumberOfCensusTracts++;
            summary.PopulationPercentChanges.Add(populationChange.PercentagePopulationChange);
            summary.UpdateAverages();
            if (!alreadyExists)
            {
                AreaBasedPopulationChanges.Add(populationChange.CoreBasedAreaCode, summary);
            }
            else
            {
                AreaBasedPopulationChanges[populationChange.CoreBasedAreaCode] = summary;
            }
        }
    }
}
