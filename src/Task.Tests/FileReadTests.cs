using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Task.Main.IO;
using Task.Main.Processing;

namespace Task.Tests
{
    /// <summary>
    /// Test class containing IO Related tests
    /// </summary>
    [TestClass]
    public class FileReadTests : BaseTest
    {
        /// <summary>
        /// Test that ensures file exist checks are correctly implemented
        /// </summary>
        [TestMethod]
        public void FileExistsTest()
        {
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader())
            {

            }
        }

        /// <summary>
        /// Test that ensures file exist checks are correctly implemented by providing an invalid path
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FileDoesNotExistTest()
        {
            string dummyFilePath = "input/dummy.csv";
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader(dummyFilePath))
            {

            }
        }

        /// <summary>
        /// Test that ensures the input file can be opened correctly
        /// </summary>
        [TestMethod]
        public void OpenFileTest()
        {
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader())
            {
                reader.OpenAndValidateFile();
            }
        }

        /// <summary>
        /// Test that ensures the input file header check works properly
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void OpenInvalidFileTest()
        {
            string excelInputFilePath = "input/censustract-00-10.xlsx";
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader(excelInputFilePath))
            {
                reader.OpenAndValidateFile();
            }
        }

        /// <summary>
        /// Test that checks if a line can be seperated correctly into columns
        /// </summary>
        [TestMethod]
        public void ParseLineTest()
        {
            string testLine = "01005950100,01,005,950100,187.3614167,11.21928403,,21640,\"Eufaula, AL - GA\",,2,C,3848,1735,3321,1627,-527,-13.70,-108,-6.22";
            string[] expectedColumns = new string[] { "01005950100", "01", "005", "950100", "187.3614167", "11.21928403", "", "21640", "\"Eufaula, AL - GA\"", "", "2", "C", "3848", "1735", "3321", "1627", "-527", "-13.70", "-108", "-6.22" };
            string[] parsedColumns = new TractPopulationChangeReader().ExtractColumnsFromLineText(testLine);

            Assert.IsTrue(Enumerable.SequenceEqual(expectedColumns, parsedColumns));
        }

        /// <summary>
        /// Test that uses a file that has a row with a missing column and checks if the validation works correctly
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DataMisalignedException))]
        public void ParseLineInvalidFileTest()
        {
            string invalidFilePath = "input/InvalidColumnsData.csv";
            using (TractPopulationChangeReader reader = new TractPopulationChangeReader(invalidFilePath))
            {
                TractPopulationChangeProccessor proccessor = new TractPopulationChangeProccessor();
                reader.OpenAndValidateFile();
                reader.Read(proccessor.ParseTextToDatum);
            }
        }
    }
}
