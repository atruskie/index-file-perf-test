namespace IndexFilePerfTest
{
    using System.Collections.Generic;
    using BenchmarkDotNet.Attributes;

    public class FileExistsBenchmark : BenchmarkBase
    {
        public string CurrentFile { get; private set; }

        [Setup]
        public void ChooseFile()
        {
            this.CurrentFile = this.manifest.GetRandomFile();
            this.UpdatePaths(this.DataSize);
        }

        [Benchmark]
        public void FileExistsFromTar()
        {
            this.tar.FileExists(this.tarFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromHdf5()
        {
            this.hdf5.FileExists(this.hdf5File, this.CurrentFile);
        }

        [Benchmark(Baseline = true)]
        public void FileExistsFromRaw()
        {
            this.raw.FileExists(this.rawFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromNoop()
        {
            this.noop.FileExists(this.rawFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromZip()
        {
            this.zip.FileExists(this.zipFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromTarApi()
        {
            this.tar.FileExistsApi(this.tarFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromHdf5Api()
        {
            this.hdf5.FileExistsApi(this.hdf5File, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteApiRowId32768()
        {
            this.sqLite.FileExistsApi(this.sqLiteFileRowId32768, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteApiNoRowId32768()
        {
            this.sqLite.FileExistsApi(this.sqLiteFileNoRowId32768, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteApiRowId16384()
        {
            this.sqLite.FileExistsApi(this.SqLiteFileRowId16384, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteApiNoRowId16384()
        {
            this.sqLite.FileExistsApi(this.sqLiteFileNoRowId16384, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteApiRowId8192()
        {
            this.sqLite.FileExistsApi(this.sqLiteFileRowId8192, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteApiNoRowId8192()
        {
            this.sqLite.FileExistsApi(this.sqLiteFileNoRowId8192, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteRowId32768()
        {
            this.sqLite.FileExists(this.sqLiteFileRowId32768, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteNoRowId32768()
        {
            this.sqLite.FileExists(this.sqLiteFileNoRowId32768, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteRowId16384()
        {
            this.sqLite.FileExists(this.SqLiteFileRowId16384, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteNoRowId16384()
        {
            this.sqLite.FileExists(this.sqLiteFileNoRowId16384, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteRowId8192()
        {
            this.sqLite.FileExists(this.sqLiteFileRowId8192, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromSqLiteNoRowId8192()
        {
            this.sqLite.FileExists(this.sqLiteFileNoRowId8192, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromRawApi()
        {
            this.raw.FileExistsApi(this.rawFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromRawNoop()
        {
            this.noop.FileExistsApi(this.rawFile, this.CurrentFile);
        }

        [Benchmark]
        public void FileExistsFromZipApi()
        {
            this.zip.FileExistsApi(this.zipFile, this.CurrentFile);
        }
    }
}