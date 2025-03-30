using System;
using System.IO;
using Xunit;

public class FileSaverTests
{
    [Fact]
    public void SaveToFile_ShouldWriteContentToFile()
    {
        // Arrange
        var fileSaver = new FileSaver();
        var testFilePath = Path.Combine(Path.GetTempPath(), "testfile.txt");
        var content = "This is a test content.";

        // Act
        fileSaver.SaveToFile(testFilePath, content);

        // Assert
        Assert.True(File.Exists(testFilePath), "File should exist after saving.");
        var fileContent = File.ReadAllText(testFilePath);
        Assert.Equal(content, fileContent);

        // Cleanup
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}

// Mock class for FileSaver (if not already available in the project)
public class FileSaver
{
    public void SaveToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content);
    }
}
