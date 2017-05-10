namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ICSharpCode.SharpZipLib.Tar;

    public class Tar : IFormatInterface
    {
        private readonly string workingDirectory;

        public Tar()
        {
            this.workingDirectory = Directory.GetCurrentDirectory();
        }

        public string GetFile(string source, string filename, string destination)
        {
            var args = $"--force-local --extract --file={source} -C {destination} {filename}";
            Runner.RunSuccessfully(this.workingDirectory, "tar", args);

            return destination;
        }

        public string GetFileApi(string source, string filename, string destination)
        {
            this.ExtractTarByEntry(source, filename, destination);

            return destination;
        }

        public bool FileExists(string source, string filename)
        {
            var args = $"-t --file={source} {filename}";
            (int result, _) = Runner.Run(this.workingDirectory, "tar", args);

            return result == 0;
        }

        public bool FileExistsApi(string source, string filename)
        {
            using (FileStream fsIn = new FileStream(source, FileMode.Open, FileAccess.Read))
            {
                TarInputStream tarIn = new TarInputStream(fsIn);
                TarEntry tarEntry;

                while ((tarEntry = tarIn.GetNextEntry()) != null)
                {
                    if (tarEntry.IsDirectory)
                    {
                        continue;
                    }

                    if (tarEntry.Name == filename)
                    {
                        return true;
                    }
                }

                tarIn.Close();
            }

            return false;
        }

        /// <summary>
        /// Tar files only support sequential access!
        /// </summary>
        /// <param name="tarFileName">The tar file</param>
        /// <param name="file">The file to extract</param>
        /// <param name="targetDir">The destination directory</param>
        public void ExtractTarByEntry(string tarFileName, string file, string targetDir)
        {
            using (FileStream fsIn = new FileStream(tarFileName, FileMode.Open, FileAccess.Read))
            {
                TarInputStream tarIn = new TarInputStream(fsIn);
                TarEntry tarEntry;

                while ((tarEntry = tarIn.GetNextEntry()) != null)
                {
                    if (tarEntry.IsDirectory)
                    {
                        continue;
                    }

                    if (tarEntry.Name != file)
                    {
                        continue;
                    }

                    // Apply further name transformations here as necessary
                    string outName = Path.Combine(targetDir, file);

                    using (FileStream outStr = new FileStream(outName, FileMode.Create))
                    {

                        tarIn.CopyEntryContents(outStr);
                    }
                }

                tarIn.Close();
            }
        }
    }
}
