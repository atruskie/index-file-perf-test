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

            // run the setup
            getFileBenchmark.ChooseFile();

            var exists = getFileBenchmark.FileExistsFromSqLiteApiRowId16384();

            Assert.IsTrue(exists);
        }
    }
}
