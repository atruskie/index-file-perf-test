using System.IO.Compression;

namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ICSharpCode.SharpZipLib.Core;
    using ICSharpCode.SharpZipLib.Zip;

    public class Zip : IFormatInterface
    {
        private readonly string workingDirectory;

        public Zip()
        {
            this.workingDirectory = Directory.GetCurrentDirectory();
        }

        public string GetFile(string source, string filename, string destination)
        {
            // -o suppresses prompts for and enables overwrites
            var args = $"-o {source} {filename} -d {destination}";
            Runner.RunSuccessfully(this.workingDirectory, "unzip", args);

            return destination;
        }

        public string GetFileApi(string source, string filename, string destination)
        {
            using (var zf = System.IO.Compression.ZipFile.OpenRead(source))
            {
                var entry = zf.GetEntry(filename);
                entry.ExtractToFile(Path.Combine(destination, entry.Name), true);
            }

            return destination;
        }

        public bool FileExists(string source, string filename)
        {
            var args = $"-Z {source} {filename}";
            (int result, _) = Runner.Run(this.workingDirectory, "unzip", args);

            return result == 0;
        }

        public bool FileExistsApi(string source, string filename)
        {
            using (var zf = System.IO.Compression.ZipFile.OpenRead(source))
            {
                return zf.GetEntry(filename) != null;
            }
        }
    }
}
