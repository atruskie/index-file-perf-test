namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Runner
    {
        public static (int ExitCode, string Output) Run(string workingDirectory, string executable, string arguments, bool getStandardOut = false)
        {
            var info = new ProcessStartInfo(executable, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = getStandardOut,
            };

            var process = Process.Start(info);

            process.WaitForExit(100000);

            if (!process.HasExited)
            {
                process.Kill();

                throw new TimeoutException($"Running {executable} with {arguments} has timed out");
            }

            string output = string.Empty;
            if (getStandardOut)
            {
                output = process.StandardOutput.ReadToEnd();
            }

            int exitCode = process.ExitCode;

            process.Dispose();

            return (exitCode, output);
        }

        public static void RunSuccessfully(string workingDirectory, string executable, string arguments)
        {
            (int result, string output) = Run(workingDirectory, executable, arguments, true);
            if (result != 0)
            {
                throw new InvalidOperationException($"Running {executable} with {arguments} has failed. Standard out: {output}");
            }
        }
    }
}
