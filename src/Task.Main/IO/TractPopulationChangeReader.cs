using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using Task.Main.Entities;

namespace Task.Main.IO
{
    /// <summary>
    /// A custom CSV file reader that is designed to read the input dataset 
    /// </summary>
    public class TractPopulationChangeReader : IDisposable
    {
        /// <summary>
        /// Path of the input file
        /// </summary>
        private string inputFilePath;

        /// <summary>
        /// File Stream of the input
        /// </summary>
        private FileStream inputFileStream;

        /// <summary>
        /// Stream Reader that reads from the File Stream
        /// </summary>
        private StreamReader inputStreamReader;

        /// <summary>
        /// The line which the reader is currently on
        /// </summary>
        private int currentLine;

        /// <summary>
        /// Default constructor of the Reader that passes the hardcoded path of the expected input file.
        /// </summary>
        public TractPopulationChangeReader() : this(Path.Combine(Constants.InputFilePath, Constants.InputFileName))
        {

        }

        /// <summary>
        /// Constructor that accepts the input file path to be changed for testing purposes.
        /// </summary>
        /// <param name="inputFilePath">Path of the input file that will be used to read</param>
        public TractPopulationChangeReader(string inputFilePath)
        {
            if (string.IsNullOrEmpty(inputFilePath))
            {
                throw new ArgumentException("Input File Path must not be empty!");
            }
            if (!File.Exists(inputFilePath))
            {
                throw new FileNotFoundException("Input file must exist in the specified path!");
            }
            this.inputFilePath = inputFilePath;
        }

        /// <summary>
        /// Closes the file properly when disposed
        /// </summary>
        public void Dispose()
        {
            if (inputFileStream != null)
            {
                inputFileStream.Close();
            }
            if (inputStreamReader != null)
            {
                inputStreamReader.Close();
            }
        }

        /// <summary>
        /// Validates the file format and opens the file
        /// </summary>
        public void OpenAndValidateFile()
        {
            inputFileStream = File.OpenRead(inputFilePath);
            inputStreamReader = new StreamReader(inputFileStream);
            string headerLine = inputStreamReader.ReadLine();
            if (!headerLine.Equals(Constants.InputFileHeaderRow))
            {
                throw new ApplicationException("Input file is invalid");
            }
            currentLine = 1;
        }

        /// <summary>
        /// Reads a single line, parses and returns it.
        /// </summary>
        /// <returns>The parsed PopulationChangeDatum if EOF is not reached, null otherwise</returns>
        public PopulationChangeDatum Read(Func<string[], PopulationChangeDatum> lineProcessorMethod)
        {
            string line = inputStreamReader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                currentLine++;
                string[] columns = ExtractColumnsFromLineText(line);
                return lineProcessorMethod(columns);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Extracts columns within the line by splitting the commas
        /// </summary>
        /// <param name="line">Full text of the line</param>
        /// <param name="overrideColumnCountCheck">Should the column count check be overridden?
        /// This flag is used to parse the result set for testing purposes</param>
        /// <returns>Extracted columns</returns>
        public string[] ExtractColumnsFromLineText(string line, bool overrideColumnCountCheck = false)
        {
            List<string> columns = new List<string>();

            //Iterate on the characters of the line and seperate them as a column if a delimeter char is found
            int lastColumnStartIndex = 0;
            bool quotationMarkActive = false;
            for (int i = 0; i < line.Length; i++)
            {
                //If quotation mark is not active and delimiter character is reached, count the characters before as a column
                if ((i == line.Length - 1)
                    || (line[i] == Constants.InputFileDelimiter && !quotationMarkActive))
                {
                    int columnLength = i - lastColumnStartIndex;
                    if (i == line.Length - 1)
                    {
                        //Include the last character only if its the last column
                        columnLength++;
                    }
                    columns.Add(line.Substring(lastColumnStartIndex, columnLength));
                    lastColumnStartIndex = i + 1;
                }

                if (line[i] == '"')
                {
                    quotationMarkActive = !quotationMarkActive;
                }
            }

            if (!overrideColumnCountCheck && columns.Count != Constants.InputFileColumnCount)
            {
                throw new DataMisalignedException($"Line {currentLine} is invalid in the input file! It has {columns.Count} instead of {Constants.InputFileColumnCount}");
            }

            return columns.ToArray();
        }
    }
}
