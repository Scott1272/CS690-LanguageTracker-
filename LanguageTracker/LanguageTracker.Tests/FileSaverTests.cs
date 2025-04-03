namespace LanguageTracker.Tests;
using LanguageTracker;
public class FileSaverTests
{
    FileSaver fileSaver;
    string testFileName;
    public FileSaverTests()   
    { 
        testFileName = "test.doc.txt";
        fileSaver = new FileSaver(testFileName);
    }    
    [Fact]
    public void Test_FileSaver_Append()
    {
        fileSaver.AppendLine("Hello World");
        var contentFromFile = File.ReadAllText(testFileName);
        Assert.Equal ("Hello World" + Environment.NewLine, contentFromFile);
    }
}