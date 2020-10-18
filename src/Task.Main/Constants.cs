using System;
using System.Collections.Generic;
using System.Text;

namespace Task.Main
{
    /// <summary>
    /// Class responsible for holding constant variable declarations
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Name of the input file
        /// </summary>
        public const string InputFileName = "censustract-00-10.csv";

        /// <summary>
        /// Path of the input folder
        /// </summary>
        public const string InputFilePath = "input";

        /// <summary>
        /// Name of the output file
        /// </summary>
        public const string OutputFileName = "report.csv";

        /// <summary>
        /// Path of the output folder
        /// </summary>
        public const string OutputFilePath = "output";

        /// <summary>
        /// Header row of the CSV that is expected in the input file
        /// </summary>
        public const string InputFileHeaderRow = "GEOID,ST10,COU10,TRACT10,AREAL10,AREAW10,CSA09,CBSA09,CBSA_T,MDIV09,CSI,COFLG,POP00,HU00,POP10,HU10,NPCHG,PPCHG,NHCHG,PHCHG";

        /// <summary>
        /// Column count of the input file that is expected
        /// </summary>
        public static int InputFileColumnCount
        {
            get
            {
                return InputFileHeaderRow.Split(InputFileDelimiter).Length;
            }
        }

        /// <summary>
        /// Delimiter that is used in the CSV input file
        /// </summary>
        public const char InputFileDelimiter = ',';

        /// <summary>
        /// Default culture to be used by the application
        /// </summary>
        public const string DefaultCulture = "en-US";

        /// <summary>
        /// Value to be ignored while checing for cell value
        /// </summary>
        public const string IgnoredColumnValue = "(X)";
    }
}
