using System;
using IndexFilePerfTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SqlLiteTests
    {
        [TestMethod]
        public void EnsureOutputCanBeParsedGet()
        {
            var getFileBenchmark = new GetFileBenchmark();

            // run the setup
            getFileBenchmark.ChooseFile();

            var outDirectory = getFileBenchmark.GetFileFromSqLiteApiRowId16384();

            Assert.AreEqual(getFileBenchmark.OutputDirectory, outDirectory);
        }

        [TestMethod]
        public void EnsureOutputCanBeParsedExists()
        {
            var getFileBenchmark = new FileExistsBenchmark();
            var sqLite = new SqLite(getFileBenchmark.SqLitePath);
            // run the setup
            getFileBenchmark.ChooseFile();

            var exists = sqLite.FileExists(getFileBenchmark.SqLiteFileRowId16384, getFileBenchmark.CurrentFile);

            Assert.IsTrue(exists);
        }
    }
}
