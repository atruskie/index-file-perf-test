using System;
using IndexFilePerfTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass()]
    public class RunnerTests
    {
        [TestMethod()]
        public void EnsureRunnerReturnsStandardOut()
        {
            var result = Runner.Run(
                Environment.CurrentDirectory, 
                "cmd", 
                "/C \"echo test && echo newline\"",
                getStandardOut: true);

            Assert.AreEqual(result.ExitCode, 0);
            Assert.AreEqual("test " + Environment.NewLine + "newline" + Environment.NewLine, result.Output);
        }
    }
}