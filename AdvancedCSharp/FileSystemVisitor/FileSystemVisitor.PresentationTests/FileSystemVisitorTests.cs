namespace FileSystemVisitor.PresentationTests
{
    public class FileSystemVisitorTests
    {
        private readonly string _path = @"..\..\..\..\FileSystemVisitor.PresentationTests\TestingFolderAndFiles";

        [Fact]
        public void Traverse_WithoutFilters_FindsFilesCorrectly()
        {
            // Arrange
            int expectedFoundFilesCount = 6;
            var fileSystemVisitor = new Presentation.FileSystemVisitor(_path);

            // Act and Assert
            Assert.Equal(expectedFoundFilesCount, fileSystemVisitor.Traverse().Count());

        }

        [Fact]
        public void Construct_WithNullubleDirectoryAndFilter_ThrowsDirectoryNotFoundException()
        {

            Assert.Throws<ArgumentException>(() => new Presentation.FileSystemVisitor(null, null));
            Assert.Throws<ArgumentException>(() => new Presentation.FileSystemVisitor(string.Empty, null));

        }

        [Fact]
        public void Construct_WithWrongDirectory_ThrowsArgumentException()
        {

            Assert.Throws<DirectoryNotFoundException>(() => new Presentation.FileSystemVisitor("C:\\NonExistentDirectory", null));

        }

        [Fact]
        public void Traverse_WithFiltersForCs_FindsFilesCorrectly()
        {
            // Arrange
            string expectedFileDirectory = @"\FileSystemVisitor.PresentationTests\TestingFolderAndFiles\Test.cs";
            var fileSystemVisitor = new Presentation.FileSystemVisitor(_path, p => p.Extension == ".cs");

            // Act and Assert
            foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
            {
                Assert.Contains(expectedFileDirectory, fileSystemInfo.FullName);
            }

        }

        [Fact]
        public void Traverse_WithFiltersForTxt_FindsFilesCorrectly()
        {
            // Arrange
            string[] expectedFileDirectories = new string[]
            { 
                @"\FileSystemVisitor.PresentationTests\TestingFolderAndFiles\TestFolder\TestFolder.txt",
                @"\FileSystemVisitor.PresentationTests\TestingFolderAndFiles\Test.txt"
            };
            var fileSystemVisitor = new Presentation.FileSystemVisitor(_path, p => p.Extension == ".txt");

            // Act and Assert
            int i = 0;
            foreach (var fileSystemInfo in fileSystemVisitor.Traverse())
            {
                Assert.Contains(expectedFileDirectories[i], fileSystemInfo.FullName);
                i++;
            }

        }
    }
}