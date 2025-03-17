using LanguageTracker;
using System;
using System.IO;
using Xunit;

namespace LanguageTracker.Tests
{
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
            // Arrange
            string expectedContent = "Hello World" + Environment.NewLine;

            try
            {            
                // Act
                fileSaver.AppendLine("Hello World");
                var contentFromFile = File.ReadAllText(testFileName);

                // Assert
                Assert.Equal(expectedContent, contentFromFile);
            }
            finally
            {
                // Clean up
                File.Delete(testFileName);
            }    
        }
    }
}