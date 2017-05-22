using BenchmarkDotNet.Attributes;

namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes.Exporters;
    using BenchmarkDotNet.Exporters.Csv;

    [CsvMeasurementsExporter(CsvSeparator.Comma)]
    [RPlotExporter]
    [JsonExporterAttribute.Full]
    public abstract class BenchmarkBase
    {
        const string DataRoot = @"C:\Temp\zoomData\";

#pragma warning disable SA1401 // Fields must be private
        protected string tarFile;
        protected string hdf5File;
        protected Manifest manifest;
        protected string zipFile;
        protected string rawFile;
        protected Hdf5 hdf5;
        protected Noop noop;
        protected Raw raw;
        protected SqLite sqLite;
        protected Tar tar;
        protected Zip zip;
        protected string sqLiteFileRowId32768;
        protected string sqLiteFileNoRowId8192;
        protected string sqLiteFileRowId8192;
        protected string sqLiteFileNoRowId16384;
        protected string sqLiteFileRowId16384;
        protected string sqLiteFileNoRowId32768;
#pragma warning restore SA1401 // Fields must be private

        protected BenchmarkBase()
        {
            this.OutputDirectory = $"{DataRoot}output";

            // use today's date as a seed - should ensure consistent replication across different machines for the
            // same day
            this.manifest = new Manifest(
                @"C:\Temp\zoomData\small_manifest.txt", 
                (int)DateTime.Now.Date.ToFileTimeUtc());

            this.SqLitePath =
                @"sqlite3.exe";

            this.tar = new Tar();
            this.hdf5 = new Hdf5();
            this.sqLite = new SqLite(this.SqLitePath);
            this.zip = new Zip();
            this.raw = new Raw();
            this.noop = new Noop();
        }

        // Allows benchmarkdotnet to automatically compare two size of files
        [Params("small", "large")]
        public string DataSize { get; set; } = "small";

        public string OutputDirectory { get; }

        public string SqLitePath { get; }

        public string SqLiteFileRowId16384 => this.sqLiteFileRowId16384;

        protected void UpdatePaths(string dataSize)
        {
            this.tarFile = $"{DataRoot}{dataSize}.tar";
            this.hdf5File = $"{DataRoot}{dataSize}.h5";
            this.sqLiteFileRowId32768 = $"{DataRoot}{dataSize}.rowid.32768.sqlite3";
            this.sqLiteFileNoRowId8192 = $"{DataRoot}{dataSize}.wo_rowid.8192.sqlite3";
            this.sqLiteFileRowId8192 = $"{DataRoot}{dataSize}.rowid.8192.sqlite3";
            this.sqLiteFileNoRowId16384 = $"{DataRoot}{dataSize}.wo_rowid.16384.sqlite3";
            this.sqLiteFileRowId16384 = $"{DataRoot}{dataSize}.rowid.16384.sqlite3";
            this.sqLiteFileNoRowId32768 = $"{DataRoot}{dataSize}.wo_rowid.32768.sqlite3";
            this.zipFile = $"{DataRoot}{dataSize}.zip";
            string rawPattern = dataSize == "small" ? ".small" : string.Empty;
            this.rawFile = $"{DataRoot}4c77b524-1857-4550-afaa-c0ebe5e3960a_101013-0000.mp3{rawPattern}";
        }
    }
}
