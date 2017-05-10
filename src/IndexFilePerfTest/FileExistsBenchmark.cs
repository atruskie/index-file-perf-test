namespace IndexFilePerfTest
{
    using System.Collections.Generic;
    using BenchmarkDotNet.Attributes;

    public class FileExistsBenchmark : BenchmarkBase
    {
        private string currentFile;

        [Setup]
        public void ChooseFile()
        {
            this.currentFile = this.manifest.GetRandomFile();
        }

        [Benchmark]
        public bool FileExistsFromTar()
        {
            return this.tar.FileExists(this.tarFile, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromHdf5()
        {
            return this.hdf5.FileExists(this.hdf5File, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromRaw()
        {
            return this.raw.FileExists(this.rawFile, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromZip()
        {
            return this.zip.FileExists(this.zipFile, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromTarApi()
        {
            return this.tar.FileExistsApi(this.tarFile, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromHdf5Api()
        {
            return this.hdf5.FileExistsApi(this.hdf5File, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteApiRowId32768()
        {
            return this.sqLite.FileExistsApi(this.sqLiteFileRowId32768, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteApiNoRowId32768()
        {
            return this.sqLite.FileExistsApi(this.sqLiteFileNoRowId32768, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteApiRowId16384()
        {
            return this.sqLite.FileExistsApi(this.sqLiteFileRowId16384, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteApiNoRowId16384()
        {
            return this.sqLite.FileExistsApi(this.sqLiteFileNoRowId16384, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteApiRowId8192()
        {
            return this.sqLite.FileExistsApi(this.sqLiteFileRowId8192, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteApiNoRowId8192()
        {
            return this.sqLite.FileExistsApi(this.sqLiteFileNoRowId8192, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteRowId32768()
        {
            return this.sqLite.FileExists(this.sqLiteFileRowId32768, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteNoRowId32768()
        {
            return this.sqLite.FileExists(this.sqLiteFileNoRowId32768, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteRowId16384()
        {
            return this.sqLite.FileExists(this.sqLiteFileRowId16384, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteNoRowId16384()
        {
            return this.sqLite.FileExists(this.sqLiteFileNoRowId16384, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteRowId8192()
        {
            return this.sqLite.FileExists(this.sqLiteFileRowId8192, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromSqLiteNoRowId8192()
        {
            return this.sqLite.FileExists(this.sqLiteFileNoRowId8192, this.currentFile);
        }

        [Benchmark(Baseline = true)]
        public bool FileExistsFromRawApi()
        {
            return this.raw.FileExistsApi(this.rawFile, this.currentFile);
        }

        [Benchmark]
        public bool FileExistsFromZipApi()
        {
            return this.zip.FileExistsApi(this.zipFile, this.currentFile);
        }
    }
}