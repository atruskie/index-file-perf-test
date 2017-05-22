namespace IndexFilePerfTest
{
    using System.Collections.Generic;
    using System.IO;
    using BenchmarkDotNet.Attributes;

    public class GetFileBenchmark : BenchmarkBase
    {
        public string CurrentFile { get; private set; }

        [Setup]
        public void ChooseFile()
        {
            this.CurrentFile = this.manifest.GetRandomFile();
            this.UpdatePaths(this.DataSize);
        }

        [Cleanup]
        public void Cleanup()
        {
            var path = Path.Combine(this.OutputDirectory, this.CurrentFile);
            File.Delete(path);
        }

        [Benchmark]
        public string GetFileFromTar()
        {
            return this.tar.GetFile(this.tarFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromHdf5()
        {
            return this.hdf5.GetFile(this.hdf5File, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteApiRowId32768()
        {
            return this.sqLite.GetFileApi(this.sqLiteFileRowId32768, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteApiNoRowId32768()
        {
            return this.sqLite.GetFileApi(this.sqLiteFileNoRowId32768, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteApiRowId16384()
        {
            return this.sqLite.GetFileApi(this.SqLiteFileRowId16384, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteApiNoRowId16384()
        {
            return this.sqLite.GetFileApi(this.sqLiteFileNoRowId16384, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteApiRowId8192()
        {
            return this.sqLite.GetFileApi(this.sqLiteFileRowId8192, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteApiNoRowId8192()
        {
            return this.sqLite.GetFileApi(this.sqLiteFileNoRowId8192, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteRowId32768()
        {
            return this.sqLite.GetFile(this.sqLiteFileRowId32768, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteNoRowId32768()
        {
            return this.sqLite.GetFile(this.sqLiteFileNoRowId32768, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteRowId16384()
        {
            return this.sqLite.GetFile(this.SqLiteFileRowId16384, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteNoRowId16384()
        {
            return this.sqLite.GetFile(this.sqLiteFileNoRowId16384, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteRowId8192()
        {
            return this.sqLite.GetFile(this.sqLiteFileRowId8192, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromSqLiteNoRowId8192()
        {
            return this.sqLite.GetFile(this.sqLiteFileNoRowId8192, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark(Baseline = true)]
        public string GetFileFromRaw()
        {
            return this.raw.GetFile(this.rawFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromNoop()
        {
            return this.noop.GetFile(this.rawFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromZip()
        {
            return this.zip.GetFile(this.zipFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromTarApi()
        {
            return this.tar.GetFileApi(this.tarFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromHdf5Api()
        {
            return this.hdf5.GetFileApi(this.hdf5File, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromRawApi()
        {
            return this.raw.GetFileApi(this.rawFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromRawNoop()
        {
            return this.noop.GetFileApi(this.rawFile, this.CurrentFile, this.OutputDirectory);
        }

        [Benchmark]
        public string GetFileFromZipApi()
        {
            return this.zip.GetFileApi(this.zipFile, this.CurrentFile, this.OutputDirectory);
        }
    }
}