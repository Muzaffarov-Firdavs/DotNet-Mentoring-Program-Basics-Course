
string path = @"..\..\..\..\FileSystemVisitor.Presentation";
var listOfFilesAndFolders = new FileSystemVisitor.Presentation.FileSystemVisitor(path, fsInfo => fsInfo.Extension == ".cs");

foreach (var fileSystemInfo in listOfFilesAndFolders.Traverse())
{
    Console.WriteLine(fileSystemInfo.FullName);
}
