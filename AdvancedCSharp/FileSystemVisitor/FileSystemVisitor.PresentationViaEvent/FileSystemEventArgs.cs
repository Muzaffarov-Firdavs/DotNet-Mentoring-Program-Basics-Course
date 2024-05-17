namespace FileSystemVisitor.PresentationViaEvent
{
    public class FileSystemEventArgs : EventArgs
    {
        public FileSystemInfo FileSystemInfo { get; }
        public bool Abort { get; set; }
        public bool Exclude { get; set; }

        public FileSystemEventArgs(FileSystemInfo fileSystemInfo)
        {
            FileSystemInfo = fileSystemInfo;
        }
    }
}