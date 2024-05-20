namespace FileSystemVisitor.PresentationViaEvent
{
    public class FileSystemEventArgs : EventArgs
    {
        public FileSystemInfo FileSystemInfo { get; }

        public FileSystemEventArgs(FileSystemInfo fileSystemInfo)
        {
            FileSystemInfo = fileSystemInfo;
        }
    }
}