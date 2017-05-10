using System;
using System.IO;
using IndexFilePerfTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Hdf5Tests
    {
        [TestMethod]
        public void EnsureItApiGetFileWorks()
        {
            var getFileBenchmark = new GetFileBenchmark();

            // run the setup
            getFileBenchmark.ChooseFile();

            var outDirectory = getFileBenchmark.GetFileFromHdf5Api();

            Assert.IsTrue(File.Exists(outDirectory + Path.DirectorySeparatorChar + getFileBenchmark.CurrentFile));
        }
    }
}
