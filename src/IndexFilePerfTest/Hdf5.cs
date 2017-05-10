namespace IndexFilePerfTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using HDF5DotNet;

    public class Hdf5 : IFormatInterface
    {
        private readonly string workingDirectory;

        public Hdf5()
        {
            this.workingDirectory = Directory.GetCurrentDirectory();
        }

        public string GetFile(string source, string filename, string destination)
        {
            var outputPath = Path.Combine(destination, filename);
            var args = $"-d \"/{filename}\" -b FILE -o \"{outputPath}\" {source}";
            Runner.RunSuccessfully(this.workingDirectory, "h5dump", args);

            return destination;
        }

        public string GetFileApi(string source, string filename, string destination)
        {
            var fileId = H5F.open(source, H5F.OpenMode.ACC_RDONLY);
            var datasetId = H5D.open(fileId, filename);
            //var datasetTypeId = new H5DataTypeId(H5T.H5Type.NATIVE_OPAQUE);
            //var space = H5D.getSpace(datasetId);
            //var dims = H5S.getSimpleExtentDims(space);
            var dataType = H5D.getType(datasetId);

            long size = H5D.getStorageSize(datasetId);
            var fileBytes = new byte[size];
            var h5Array = new H5Array<byte>(fileBytes);
            H5D.read(datasetId, dataType, h5Array);
            H5D.close(datasetId);
            H5F.close(fileId);

            var outPath = Path.Combine(destination, filename);
            File.WriteAllBytes(outPath, fileBytes);

            return destination;
        }

        public bool FileExists(string source, string filename)
        {
            var args = $"\"{source}/{filename}\"";
            (int result, _) = Runner.Run(this.workingDirectory, "h5ls", args);

            return result == 0;
        }

        public bool FileExistsApi(string source, string filename)
        {
            var fileId = H5F.open(source, H5F.OpenMode.ACC_RDONLY);
            var result = H5L.Exists(fileId, filename);

            H5F.close(fileId);

            return result;
        }
    }
}
