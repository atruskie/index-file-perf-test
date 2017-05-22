namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This class is used to assert performance overheads - particuarly for creating a new process which is expensive
    /// on Windows.
    /// It doesn't do anything other than a few base funtion calls that are common to the other interfaces
    /// and some basic attempts to avoid method optimization.
    /// </summary>
    public class Noop : IFormatInterface
    {
        private readonly string workingDirectory;

        public Noop()
        {
            this.workingDirectory = Directory.GetCurrentDirectory();
        }

        public string GetFile(string source, string filename, string destination)
        {
            var args = Path.Combine(source, filename) + " " + destination;
            Runner.RunSuccessfully(this.workingDirectory, "echo", args);

            return destination;
        }

        public string GetFileApi(string source, string filename, string destination)
        {
            var paths = Path.Combine(source, filename) + Path.Combine(destination, filename);
            return paths;
        }

        public bool FileExists(string source, string filename)
        {
            Runner.RunSuccessfully(this.workingDirectory, "echo", Path.Combine(source, filename));

            return true;
        }

        public bool FileExistsApi(string source, string filename)
        {
            return Path.Combine(source, filename).Length > 0;
        }
    }
}
