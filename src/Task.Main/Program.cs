using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Threading;
using Task.Main.Processing;

namespace Task.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                Console.WriteLine("Process starting...");
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(Constants.DefaultCulture);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Constants.DefaultCulture);
                new TractPopulationChangeSummarizer().SummarizeDataSet();
                stopwatch.Stop();
                Console.WriteLine($"Process ended in {stopwatch.ElapsedMilliseconds} milliseconds.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(-1);
            }
        }
    }
}
