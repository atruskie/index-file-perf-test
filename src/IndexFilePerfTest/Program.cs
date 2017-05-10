using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Jobs;

namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Exporters.Csv;
    using BenchmarkDotNet.Reports;
    using BenchmarkDotNet.Running;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Initiating benchmarking");

            var config = ManualConfig
                .Create(DefaultConfig.Instance);

            // ensure numbers in csv do not have units
            var csvExporter = new CsvExporter(
                CsvSeparator.CurrentCulture,
                new SummaryStyle()
                {
                    PrintUnitsInHeader = true,
                    PrintUnitsInContent = false,
                    TimeUnit = BenchmarkDotNet.Horology.TimeUnit.Millisecond,
                });
            config.Add(csvExporter);

            config.Add(
                Job.Default.WithRemoveOutliers(false)
            );

            var switcher = new BenchmarkSwitcher(new[] { typeof(GetFileBenchmark), typeof(FileExistsBenchmark) });
            var summary = switcher.Run(args, config);

            Console.WriteLine("Benchmarking complete");
        }
    }
}