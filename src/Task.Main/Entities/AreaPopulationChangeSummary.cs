using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Main.Entities
{
    public class AreaPopulationChangeSummary
    {
        /// <summary>
        /// Default constructor that sets the Area Code and Description
        /// </summary>
        /// <param name="areaCode">Core Based Statstical Area Code</param>
        /// <param name="areaDescription">Core Based Statistical Area Code Title</param>
        public AreaPopulationChangeSummary(string areaCode, string areaDescription)
        {
            this.AreaCode = areaCode;
            this.AreaDescription = areaDescription;
            PopulationPercentChanges = new List<double>();
        }

        /// <summary>
        /// Core Based Statstical Area Code (i.e., CBSA09)
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// Core Based Statistical Area Code Title (i.e., CBSA_T)
        /// </summary>
        public string AreaDescription { get; set; }

        /// <summary>
        /// Total number of census tracts
        /// </summary>
        public int NumberOfCensusTracts { get; set; }

        /// <summary>
        /// Total population in the CBSA in 2000
        /// </summary>
        public long TotalPopulationIn2000 { get; set; }

        /// <summary>
        /// Total population in the CBSA in 2010
        /// </summary>
        public long TotalPopulationIn2010 { get; set; }

        /// <summary>
        /// average population percent change for census tracts in this CBSA.
        /// </summary>
        public double AveragePopulationPercentChange { get; set; }

        /// <summary>
        /// List of all population change percentages belonging to the area
        /// </summary>
        public List<double> PopulationPercentChanges { get; set; }

        public void UpdateAverages()
        {
            double sum = 0;
            foreach (double populationChangePercentage in PopulationPercentChanges)
            {
                sum += populationChangePercentage;
            }
            AveragePopulationPercentChange = sum / NumberOfCensusTracts;
        }
    }
}
