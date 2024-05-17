namespace FileSystemVisitor.Presentation
{
    public class FileSystemVisitor
    {
        private readonly string _folderPath;
        private readonly Func<FileSystemInfo, bool> _filter;

        public FileSystemVisitor(string folderPath) : this(folderPath, null) { }

        public FileSystemVisitor(string folderPath, Func<FileSystemInfo, bool> filter)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Folder path cannot be null or empty", nameof(folderPath));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The directory '{folderPath}' does not exist.");

            _folderPath = folderPath;
            _filter = filter ?? (fsInfo => true);
        }

        public IEnumerable<FileSystemInfo> Traverse()
        {
            return TraverseDirectory(new DirectoryInfo(_folderPath));
        }

        private IEnumerable<FileSystemInfo> TraverseDirectory(DirectoryInfo directory)
        {
            foreach (var fileSystemInfo in directory.EnumerateFileSystemInfos())
            {
                if (_filter(fileSystemInfo))
                    yield return fileSystemInfo;

                if (fileSystemInfo is DirectoryInfo subDirectory)
                {
                    foreach (var subileSystemInfo in TraverseDirectory(subDirectory))
                    {
                        if (_filter(subileSystemInfo))
                            yield return subileSystemInfo;
                    }
                }
            }
        }
    }
}