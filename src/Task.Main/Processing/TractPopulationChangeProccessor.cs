using System;
using Task.Main.Entities;

namespace Task.Main.Processing
{
    public class TractPopulationChangeProccessor
    {
        /// <summary>
        /// Parses the columns based on the below table
        /// Variable	Variable Description
        ///GEOID	        Concatenated State-County-Census Tract Code
        ///ST10	        State FIPS Code
        ///COU10	        County FIPS Code
        ///TRACT10	    Census Tract Code
        ///AREAL10	    Land area (square miles)
        ///AREAW10	    Water area (square miles)
        ///CSA09	        Combined Statistical Area Code
        ///CBSA09	    Core Based Statistical Area (CBSA) Code
        ///CBSA_T	    Core Based Statistical Area Title
        ///MDIV09	    Metropolitan Division Code
        ///CSI	        CBSA Status Indicator (1=Metropolitan statistical area, 2=Micropolitan statistical area, 3=Outside CBSA)
        ///COFLG	        Central/Outlying County Flag (C=Central county, O=Outlying county)
        ///POP00	        Total population (2000)
        ///HU00	        Total housing units (2000)
        ///POP10	        Total population (2010)
        ///HU10	        Total housing units (2010)
        ///NPCHG	        Numeric population change: 2000 to 2010
        ///PPCHG	        Percent population change: 2000 to 2010
        ///NHCHG	        Numeric change in housing units: 2000 to 2010
        ///PHCHG	        Percent change in housing units: 2000 to 2010
        /// </summary>
        /// <param name="lineColumns">Columns that were splitted in the current line/row</param>
        /// <returns>Parsed change information</returns>
        public PopulationChangeDatum ParseTextToDatum(string[] lineColumns)
        {
            string geoID = lineColumns[0];
            string stateFipsCode = lineColumns[1];
            string countyFipsCode = lineColumns[2];
            string censusTractCode = lineColumns[3];
            string landAreaString = NormalizeNumericValue(lineColumns[4]);
            double landArea = 0;
            if (!string.IsNullOrEmpty(landAreaString) && landAreaString != Constants.IgnoredColumnValue
                && !double.TryParse(landAreaString, out landArea))
            {
                throw new ArithmeticException($"Land Area info is set to {landAreaString} which is invalid!");
            }
            double waterArea = 0;
            string waterAreaString = NormalizeNumericValue(lineColumns[5]);
            if (!string.IsNullOrEmpty(waterAreaString) && waterAreaString != Constants.IgnoredColumnValue
                && !double.TryParse(waterAreaString, out waterArea))
            {
                throw new ArithmeticException($"Water Area info is set to {waterAreaString} which is invalid!");
            }
            string combinedAreaCode = lineColumns[6];
            string coreBasedAreaCode = lineColumns[7];
            string coreBasedAreaTitle = lineColumns[8];
            string metropolitanDivisionCode = lineColumns[9];
            string cbsaStatusIndicatorString = lineColumns[10];
            int cbsaStatusIndicator = 0;
            if (!string.IsNullOrEmpty(cbsaStatusIndicatorString) && cbsaStatusIndicatorString != Constants.IgnoredColumnValue
                && !int.TryParse(cbsaStatusIndicatorString, out cbsaStatusIndicator))
            {
                throw new ArithmeticException($"CBSA Status Indicator is set to {cbsaStatusIndicatorString} which is invalid!");
            }

            string centralOrOutlyingCountyFlag = lineColumns[11];

            string totalPopulationIn2000String = NormalizeNumericValue(lineColumns[12]);
            int totalPopulationIn2000 = 0;
            if (!string.IsNullOrEmpty(totalPopulationIn2000String) && totalPopulationIn2000String != Constants.IgnoredColumnValue
                && !int.TryParse(totalPopulationIn2000String, out totalPopulationIn2000))
            {
                throw new ArithmeticException($"Total Population in 2000 is set to {totalPopulationIn2000String} which is invalid!");
            }

            string totalHousingUnitsIn2000String = NormalizeNumericValue(lineColumns[13]);
            int totalHousingUnitsIn2000 = 0;
            if (!string.IsNullOrEmpty(totalHousingUnitsIn2000String) && totalHousingUnitsIn2000String != Constants.IgnoredColumnValue
                && !int.TryParse(totalHousingUnitsIn2000String, out totalHousingUnitsIn2000))
            {
                throw new ArithmeticException($"Total Housing Units in 2000 is set to {totalHousingUnitsIn2000String} which is invalid!");
            }

            string totalPopulationIn2010String = NormalizeNumericValue(lineColumns[14]);
            int totalPopulationIn2010 = 0;
            if (!string.IsNullOrEmpty(totalPopulationIn2010String) && totalPopulationIn2010String != Constants.IgnoredColumnValue
                && !int.TryParse(totalPopulationIn2010String, out totalPopulationIn2010))
            {
                throw new ArithmeticException($"Total Population in 2010 is set to {totalPopulationIn2010String} which is invalid!");
            }

            string totalHousingUnitsIn2010String = NormalizeNumericValue(lineColumns[15]);
            int totalHousingUnitsIn2010 = 0;
            if (!string.IsNullOrEmpty(totalHousingUnitsIn2010String) && totalHousingUnitsIn2010String != Constants.IgnoredColumnValue
                && !int.TryParse(totalHousingUnitsIn2010String, out totalHousingUnitsIn2010))
            {
                throw new ArithmeticException($"Total Housing Units in 2010 is set to {totalHousingUnitsIn2010String} which is invalid!");
            }

            string numericPopulationChangeString = NormalizeNumericValue(lineColumns[16]);
            int numericPopulationChange = 0;
            if (!string.IsNullOrEmpty(numericPopulationChangeString) && numericPopulationChangeString != Constants.IgnoredColumnValue
                && !int.TryParse(numericPopulationChangeString, out numericPopulationChange))
            {
                throw new ArithmeticException($"Numeric Population Change is set to {numericPopulationChangeString} which is invalid!");
            }

            string percentPopulationChangeString = NormalizeNumericValue(lineColumns[17]);
            double percentPopulationChange = 0;
            if (!string.IsNullOrEmpty(percentPopulationChangeString) && percentPopulationChangeString != Constants.IgnoredColumnValue
                && !double.TryParse(percentPopulationChangeString, out percentPopulationChange))
            {
                throw new ArithmeticException($"Percent Population Change is set to {percentPopulationChangeString} which is invalid!");
            }

            string numericHousingUnitsChangeString = NormalizeNumericValue(lineColumns[18]);
            int numericHousingUnitsChange = 0;
            if (!string.IsNullOrEmpty(numericHousingUnitsChangeString) && numericHousingUnitsChangeString != Constants.IgnoredColumnValue
                && !int.TryParse(numericHousingUnitsChangeString, out numericHousingUnitsChange))
            {
                throw new ArithmeticException($"Numeric Housing Units Change is set to {numericHousingUnitsChangeString} which is invalid!");
            }

            string percentHousingUnitsChangeString = NormalizeNumericValue(lineColumns[19]);
            double percentHousingUnitsChange = 0;
            if (!string.IsNullOrEmpty(percentPopulationChangeString) && percentHousingUnitsChangeString != Constants.IgnoredColumnValue
                && !double.TryParse(percentHousingUnitsChangeString, out percentHousingUnitsChange))
            {
                throw new ArithmeticException($"Percent Housing Units Change is set to {percentHousingUnitsChangeString} which is invalid!");
            }

            return new PopulationChangeDatum
            {
                GeoID = geoID,
                StateFIPSCode = stateFipsCode,
                CountyFIPSCode = countyFipsCode,
                TractCode = censusTractCode,
                LandArea = landArea,
                WaterArea = waterArea,
                CBSAStatusIndicator = cbsaStatusIndicator,
                CentralOrOutlyingCountryFlag = centralOrOutlyingCountyFlag,
                CombinedAreaCode = combinedAreaCode,
                CoreBasedAreaCode = coreBasedAreaCode,
                CoreBasedAreaTitle = coreBasedAreaTitle,
                MetropolitanDivisionCode = metropolitanDivisionCode,
                NumericHousingUnitsChange = numericHousingUnitsChange,
                NumericPopulationChange = numericPopulationChange,
                PercentageHousingUnitsChange = percentHousingUnitsChange,
                PercentagePopulationChange = percentPopulationChange,
                TotalHousingUnitsIn2000 = totalHousingUnitsIn2000,
                TotalHousingUnitsIn2010 = totalHousingUnitsIn2010,
                TotalPopulationIn2000 = totalPopulationIn2000,
                TotalPopulationIn2010 = totalPopulationIn2010
            };
        }

        /// <summary>
        /// If the numeric value expected is inside the quotation marks, removes them
        /// </summary>
        /// <param name="rawValue">Raw value that was read from the line</param>
        /// <returns>The value without quotation marks</returns>
        private string NormalizeNumericValue(string rawValue)
        {
            if (!string.IsNullOrEmpty(rawValue) && rawValue.StartsWith("\"") && rawValue.EndsWith("\""))
            {
                return rawValue.Substring(1, rawValue.Length - 2);
            }
            else
            {
                return rawValue;
            }
        }
    }
}
