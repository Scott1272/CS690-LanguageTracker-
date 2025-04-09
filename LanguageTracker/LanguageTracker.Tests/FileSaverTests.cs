using System;
using System.IO;
using Xunit;

namespace LanguageTracker.Tests
{
    public class FileSaverTests : IDisposable
    {
        private readonly string testFileName = "testFile.txt";
        private readonly FileSaver fileSaver;

        public FileSaverTests()
        {
            fileSaver = new FileSaver(testFileName);
        }

        // Cleanup logic to delete the test file after each test
        public void Dispose()
        {
            if (File.Exists(testFileName))
            {
                File.Delete(testFileName);
            }
        }

        [Fact]
        public void Test_FileSaver_Append()
        {
            // Append a line to the file
            fileSaver.AppendLine("Hello World");
            // Read the content from the file
            var contentFromFile = File.ReadAllText(testFileName);
            Assert.Equal("Hello World" + Environment.NewLine, contentFromFile);
        }

        [Fact]
        public void Test_FileSaver_WordExists_ReturnsFalse_WhenFileDoesNotExist()
        {
            // Check if the word exists in the file
            var result = fileSaver.WordExists("test");
            Assert.False(result);
        }

        [Fact]
        public void Test_FileSaver_WordExists_ReturnsTrue_WhenWordFound()
        {
            // Append a line with the word to the file
            fileSaver.AppendLine("test:1:2023-10-01");
            var result = fileSaver.WordExists("test");
            Assert.True(result);
        }

        [Fact]
        public void Test_FileSaver_UpdateWord_UpdatesExistingWord()
        {
            // Append a line with the word to the file
            fileSaver.AppendLine("test:1:2023-10-01");
            // Update the word in the file
            fileSaver.UpdateWord("test", 2, "2023-10-02");
            var contentFromFile = File.ReadAllText(testFileName);
            Assert.Equal("test:2:2023-10-02" + Environment.NewLine, contentFromFile);
        }

        [Fact]
        public void Test_FileSaver_UpdateWord_DoesNotUpdate_WhenFileDoesNotExist()
        {
            // Attempt to update a word in the file
            fileSaver.UpdateWord("test", 2, "2023-10-02");
            // Check if the word exists in the file
            var result = fileSaver.WordExists("test");
            Assert.False(result);
        }
    }
}