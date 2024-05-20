namespace FileSystemVisitor.PresentationViaEvent
{
    public class FileSystemVisitor
    {
        private readonly string _folderPath;
        private readonly Func<FileSystemInfo, bool> _filter;

        public bool Abort { get; set; }
        public bool Exclude { get; set; }

        public event EventHandler Start;
        public event EventHandler Finish;
        public event EventHandler<FileSystemEventArgs> FileFound;
        public event EventHandler<FileSystemEventArgs> DirectoryFound;
        public event EventHandler<FileSystemEventArgs> FilteredFileFound;
        public event EventHandler<FileSystemEventArgs> FilteredDirectoryFound;

        public FileSystemVisitor(string folderPath) : this(folderPath, null) { }

        public FileSystemVisitor(string folderPath, Func<FileSystemInfo, bool> filter, bool abort = false, bool exclude = false)
        {
            if (string.IsNullOrEmpty(folderPath))
                throw new ArgumentException("Start path cannot be null or empty", nameof(folderPath));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The directory '{folderPath}' does not exist.");

            _folderPath = folderPath;
            _filter = filter ?? (fsInfo => true);
            Abort = abort;
            Exclude = exclude;
        }

        public IEnumerable<FileSystemInfo> Traverse()
        {
            OnStart(EventArgs.Empty);
            foreach (var fsInfo in TraverseDirectory(new DirectoryInfo(_folderPath)))
            {
                yield return fsInfo;
            }
            OnFinish(EventArgs.Empty);
        }

        private IEnumerable<FileSystemInfo> TraverseDirectory(DirectoryInfo directory)
        {
            foreach (var fileSystemInfo in directory.EnumerateFileSystemInfos())
            {
                var eventArgs = new FileSystemEventArgs(fileSystemInfo);

                if (Abort && !_filter(fileSystemInfo))
                {
                    if (fileSystemInfo is FileInfo)
                    {
                        OnFileFound(eventArgs);
                    }
                    else if (fileSystemInfo is DirectoryInfo)
                    {
                        OnDirectoryFound(eventArgs);
                    }
                }

                if (Exclude && _filter(fileSystemInfo))
                {
                    if (fileSystemInfo is FileInfo)
                    {
                        OnFilteredFileFound(eventArgs);
                    }
                    else if (fileSystemInfo is DirectoryInfo)
                    {
                        OnFilteredDirectoryFound(eventArgs);
                    }
                }

                if (!Abort && !Exclude)
                {
                    if (_filter(fileSystemInfo))
                    {
                        if (fileSystemInfo is FileInfo)
                        {
                            OnFilteredFileFound(eventArgs);
                        }
                        else if (fileSystemInfo is DirectoryInfo)
                        {
                            OnFilteredDirectoryFound(eventArgs);
                        }
                    }
                    else
                    {
                        if (fileSystemInfo is FileInfo)
                        {
                            OnFileFound(eventArgs);
                        }
                        else if (fileSystemInfo is DirectoryInfo)
                        {
                            OnDirectoryFound(eventArgs);
                        }
                    }
                }

                if (fileSystemInfo is DirectoryInfo subDirectory)
                {
                    foreach (var subFileSystemInfo in TraverseDirectory(subDirectory))
                    {
                        yield return subFileSystemInfo;
                    }
                }
            }
        }

        protected virtual void OnStart(EventArgs e)
        {
            Start?.Invoke(this, e);
        }

        protected virtual void OnFinish(EventArgs e)
        {
            Finish?.Invoke(this, e);
        }

        protected virtual void OnFileFound(FileSystemEventArgs e)
        {
            FileFound?.Invoke(this, e);
        }

        protected virtual void OnDirectoryFound(FileSystemEventArgs e)
        {
            DirectoryFound?.Invoke(this, e);
        }

        protected virtual void OnFilteredFileFound(FileSystemEventArgs e)
        {
            FilteredFileFound?.Invoke(this, e);
        }

        protected virtual void OnFilteredDirectoryFound(FileSystemEventArgs e)
        {
            FilteredDirectoryFound?.Invoke(this, e);
        }
    }
}