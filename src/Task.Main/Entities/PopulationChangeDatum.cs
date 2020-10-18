using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Main.Entities
{
    /// <summary>
    /// Representation of a single data record in the Population Change Dataset
    /// </summary>
    public class PopulationChangeDatum
    {
        /// <summary>
        /// GEOID	Concatenated State-County-Census Tract Code
        /// </summary>
        public string GeoID { get; set; }

        /// <summary>
        /// ST10	State FIPS Code
        /// </summary>
        public string StateFIPSCode { get; set; }

        /// <summary>
        /// COU10	County FIPS Code
        /// </summary>
        public string CountyFIPSCode { get; set; }

        /// <summary>
        /// TRACT10	Census Tract Code
        /// </summary>
        public string TractCode { get; set; }

        /// <summary>
        /// AREAL10	Land area (square miles)
        /// </summary>
        public double LandArea { get; set; }

        /// <summary>
        /// AREAW10	Water area (square mi
        /// </summary>
        public double WaterArea { get; set; }

        /// <summary>
        /// CSA09	Combined Statistical Area Code
        /// </summary>
        public string CombinedAreaCode { get; set; }

        /// <summary>
        /// CBSA09	Core Based Statistical Area (CBSA) Code
        /// </summary>
        public string CoreBasedAreaCode { get; set; }

        /// <summary>
        /// CBSA_T	Core Based Statistical Area Title
        /// </summary>
        public string CoreBasedAreaTitle { get; set; }

        /// <summary>
        /// MDIV09	Metropolitan Division Code
        /// </summary>
        public string MetropolitanDivisionCode { get; set; }

        /// <summary>
        /// CSI	CBSA Status Indicator (1=Metropolitan statistical area, 2=Micropolitan statistical area, 3=Outside CBSA)
        /// </summary>
        public int CBSAStatusIndicator { get; set; }

        /// <summary>
        /// COFLG	Central/Outlying County Flag (C=Central county, O=Outlying county)
        /// </summary>
        public string CentralOrOutlyingCountryFlag { get; set; }

        /// <summary>
        /// POP00	Total population (2000)
        /// </summary>
        public int TotalPopulationIn2000 { get; set; }

        /// <summary>
        /// HU00	Total housing units (2000)
        /// </summary>
        public int TotalHousingUnitsIn2000 { get; set; }


        /// <summary>
        /// POP10	Total population (2010)
        /// </summary>
        public int TotalPopulationIn2010 { get; set; }

        /// <summary>
        /// HU10	Total housing units (2010)
        /// </summary>
        public int TotalHousingUnitsIn2010 { get; set; }

        /// <summary>
        /// NPCHG	Numeric population change: 2000 to 2010
        /// </summary>
        public int NumericPopulationChange { get; set; }

        /// <summary>
        /// PPCHG	Percent population change: 2000 to 2010
        /// </summary>
        public double PercentagePopulationChange { get; set; }

        /// <summary>
        /// NHCHG	Numeric change in housing units: 2000 to 2010
        /// </summary>
        public int NumericHousingUnitsChange { get; set; }

        /// <summary>
        /// PHCHG	Percent change in housing units: 2000 to 2010
        /// </summary>
        public double PercentageHousingUnitsChange { get; set; }
    }
}
