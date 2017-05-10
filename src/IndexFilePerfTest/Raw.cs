namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Raw : IFormatInterface
    {
        private readonly string workingDirectory;

        public Raw()
        {
            this.workingDirectory = Directory.GetCurrentDirectory();
        }

        public string GetFile(string source, string filename, string destination)
        {
            var args = Path.Combine(source, filename) + " " + destination;
            Runner.RunSuccessfully(this.workingDirectory, "cp", args);

            return destination;
        }

        public string GetFileApi(string source, string filename, string destination)
        {
            File.Copy(Path.Combine(source, filename), Path.Combine(destination, filename), true);
            return destination;
        }

        public bool FileExists(string source, string filename)
        {
            (int result, _) = Runner.Run(this.workingDirectory, "ls", Path.Combine(source, filename));

            return result == 0;
        }

        public bool FileExistsApi(string source, string filename)
        {
            return File.Exists(Path.Combine(source, filename));
        }
    }
}
