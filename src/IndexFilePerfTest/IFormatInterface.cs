namespace IndexFilePerfTest
{
    public interface IFormatInterface
    {
        string GetFile(string source, string filename, string destination);

        string GetFileApi(string source, string filename, string destination);

        bool FileExists(string source, string filename);

        bool FileExistsApi(string source, string filename);
    }
}
