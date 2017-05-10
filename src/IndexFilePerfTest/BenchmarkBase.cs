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
#pragma warning disable SA1401 // Fields must be private
        protected string tarFile;
        protected string hdf5File;
        protected Manifest manifest;
        protected string zipFile;
        protected string rawFile;
        protected Hdf5 hdf5;
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
            var dataRoot = @"C:\Temp\zoomData\";
            var dataSize = "small";

            this.OutputDirectory = $"{dataRoot}output";

            this.manifest = new Manifest(@"C:\Temp\zoomData\small_manifest.txt");

            this.SqLitePath =
                @"C:\Work\Github\index-file-perf-test\bin\sqlite-tools-win32-x86-3170000\sqlite3.exe";

            this.tarFile = $"{dataRoot}{dataSize}.tar";
            this.hdf5File = $"{dataRoot}{dataSize}.h5";
            this.sqLiteFileRowId32768 = $"{dataRoot}{dataSize}.rowid.32768.sqlite3";
            this.sqLiteFileNoRowId8192 = $"{dataRoot}{dataSize}.wo_rowid.8192.sqlite3";
            this.sqLiteFileRowId8192 = $"{dataRoot}{dataSize}.rowid.8192.sqlite3";
            this.sqLiteFileNoRowId16384 = $"{dataRoot}{dataSize}.wo_rowid.16384.sqlite3";
            this.sqLiteFileRowId16384 = $"{dataRoot}{dataSize}.rowid.16384.sqlite3";
            this.sqLiteFileNoRowId32768 = $"{dataRoot}{dataSize}.wo_rowid.32768.sqlite3";
            this.zipFile = $"{dataRoot}{dataSize}.zip";
            this.rawFile = $"{dataRoot}4c77b524-1857-4550-afaa-c0ebe5e3960a_101013-0000.mp3.{dataSize}";

            this.tar = new Tar();
            this.hdf5 = new Hdf5();
            this.sqLite = new SqLite(this.SqLitePath);
            this.zip = new Zip();
            this.raw = new Raw();
        }

        public string OutputDirectory { get; }

        public string SqLitePath { get; }
    }
}
