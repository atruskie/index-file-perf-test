namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using BenchmarkDotNet.Loggers;

    public class SqLite : IFormatInterface
    {
        private readonly string sqLitePath;
        private readonly string workingDirectory;

        public SqLite(string sqLitePath)
        {
            this.sqLitePath = sqLitePath;
            this.workingDirectory = Directory.GetCurrentDirectory();

            string result;
            using (var connection = new SQLiteConnection("Data Source=:memory:;"))
            {
                connection.Open();
                var command = new SQLiteCommand("select sqlite_version();", connection);
                result = (string)command.ExecuteScalar();
            }

            var versionMessage = $@"
// It has proven exceedingly difficult to track down an up-to-date version of
// SQLite for dotnet. Thus on class initialization I'm going to actively 
// output the current version by querying the database. The current version is:
// ===========================================================================
// 
// {result}
// 
// ===========================================================================";

            ConsoleLogger.Default.WriteLine(LogKind.Info, versionMessage);
        }

        public string GetFile(string source, string filename, string destination)
        {
            var outputPath = Path.Combine(destination, filename);
            var args = $"-batch  {source} \"SELECT writefile('{outputPath}', blob) FROM file_list WHERE filename = '{filename}'";
            (int result, string output) = Runner.Run(this.workingDirectory, this.sqLitePath, args, getStandardOut: true);

            if (result == 0 && int.Parse(output.Trim()) > 0)
            {
                return destination;
            }

            throw new InvalidOperationException($"Extracting file {filename} from {source} has failed");
        }

        public string GetFileApi(string source, string filename, string destination)
        {
            using (var connection = new SQLiteConnection($"Data Source={source};Mode=ReadOnly;"))
            {
                connection.Open();
                var outputPath = Path.Combine(destination, filename);
                var query = $"SELECT blob FROM file_list WHERE filename = '{filename}'";
                var command = new SQLiteCommand(query, connection);
                var result = command.ExecuteScalar();

                if (result == null)
                {
                    throw new InvalidOperationException();
                }

                File.WriteAllBytes(outputPath, (byte[])result);

                return destination;
            }
        }

        private static byte[] GetBytes(SQLiteDataReader reader)
        {
            const int chunkSize = 2 * 1024;
            byte[] buffer = new byte[chunkSize];
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                long bytesRead;
                while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    stream.Write(buffer, 0, (int)bytesRead);
                    fieldOffset += bytesRead;
                }

                return stream.ToArray();
            }
        }

        public bool FileExists(string source, string filename)
        {
            var args = $"-batch  {source} \"SELECT EXISTS(SELECT 1 FROM file_list WHERE filename = '{filename}' LIMIT 1)";
            (int result, string output) = Runner.Run(this.workingDirectory, this.sqLitePath, args, getStandardOut: true);

            if (result != 0)
            {
                return false;
            }

            ConsoleLogger.Default.WriteLine(LogKind.Default, output);
            return int.Parse(output.Trim()) == 1;
        }

        public bool FileExistsApi(string source, string filename)
        {
            using (new SQLiteConnection($"Data Source={source};Mode=ReadOnly;"))
            {
                var command = new SQLiteCommand($"SELECT EXISTS(SELECT 1 FROM file_list WHERE filename = '{filename}' LIMIT 1)");
                var result = (int)command.ExecuteScalar();
                return result == 1;
            }
        }
    }
}
