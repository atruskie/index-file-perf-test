using System.IO;
using BenchmarkDotNet.Loggers;

namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Manifest
    {
        private readonly Random random;
        private readonly string[] files;

        public Manifest(string manifest, int? seed = null)
        {
            seed = seed ?? Environment.TickCount;
            this.random = new Random(seed.Value);
            ConsoleLogger.Default.WriteLine(LogKind.Info, $"// Random seed used: {seed.Value}");

            ConsoleLogger.Default.WriteLine(LogKind.Info, "// Reading in file manifest");
            this.files = File.ReadAllLines(manifest);
        }

        public string GetRandomFile()
        {
            int index = this.random.Next(0, this.files.Length);
            return this.files[index];
        }
    }
}
