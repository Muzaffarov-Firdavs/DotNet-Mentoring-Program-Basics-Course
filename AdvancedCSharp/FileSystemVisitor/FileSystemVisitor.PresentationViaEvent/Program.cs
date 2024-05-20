
string path = @"..\..\..\..\FileSystemVisitor.PresentationViaEvent";

var visitor = new FileSystemVisitor.PresentationViaEvent.FileSystemVisitor(path, fsInfo => fsInfo.Extension == ".cs", false, true);

visitor.Start += (s, e) => Console.WriteLine("Search started. =============================>");
visitor.Finish += (s, e) => Console.WriteLine("<==============Search finished.");
visitor.FileFound += (s, e) => Console.WriteLine($"==============\n\n\nFile found: {e.FileSystemInfo.FullName} \n\n\n");
visitor.DirectoryFound += (s, e) => Console.WriteLine($"==============\n\n\nDirectory found: {e.FileSystemInfo.FullName} \n\n\n");
visitor.FilteredFileFound += (s, e) => Console.WriteLine($"==============\n\n\nFiltered file found: {e.FileSystemInfo.FullName} \n\n\n");
visitor.FilteredDirectoryFound += (s, e) => Console.WriteLine($"==============\n\n\nFiltered directory found: {e.FileSystemInfo.FullName} \n\n\n");

foreach (var fileSystem in visitor.Traverse())
{
    Console.WriteLine(fileSystem.FullName);
}
