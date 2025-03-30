using System;
using System.IO;
using Xunit;

namespace LanguageTracker.Tests
{
    public class FileSaverTests
    {
        private readonly string testFilePath;

        public FileSaverTests()
        {
            // Use a temporary file for testing
            testFilePath = Path.Combine(Path.GetTempPath(), "testFileSaver.txt");
        }

        [Fact]
        public void AppendLine_ShouldAddLineToFile()
        {
            // Arrange
            var fileSaver = new FileSaver(testFilePath);
            string lineToAppend = "Hello, World!";
            string expectedContent = lineToAppend + Environment.NewLine;

            try
            {
                // Ensure the file is clean before the test
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }

                // Act
                fileSaver.AppendLine(lineToAppend);
                string fileContent = File.ReadAllText(testFilePath);

                // Assert
                Assert.Equal(expectedContent, fileContent);
            }
            finally
            {
                // Clean up
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }
            }
        }

        [Fact]
        public void AppendLine_MultipleLines_ShouldAppendAllLines()
        {
            // Arrange
            var fileSaver = new FileSaver(testFilePath);
            string[] linesToAppend = { "First Line", "Second Line", "Third Line" };
            string expectedContent = string.Join(Environment.NewLine, linesToAppend) + Environment.NewLine;

            try
            {
                // Ensure the file is clean before the test
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }

                // Act
                foreach (var line in linesToAppend)
                {
                    fileSaver.AppendLine(line);
                }
                string fileContent = File.ReadAllText(testFilePath);

                // Assert
                Assert.Equal(expectedContent, fileContent);
            }
            finally
            {
                // Clean up
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }
            }
        }

        [Fact]
        public void AppendLine_ShouldCreateFileIfNotExists()
        {
            // Arrange
            var fileSaver = new FileSaver(testFilePath);
            string lineToAppend = "This is a new file.";
            string expectedContent = lineToAppend + Environment.NewLine;

            try
            {
                // Ensure the file does not exist before the test
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }

                // Act
                fileSaver.AppendLine(lineToAppend);
                string fileContent = File.ReadAllText(testFilePath);

                // Assert
                Assert.Equal(expectedContent, fileContent);
            }
            finally
            {
                // Clean up
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }
            }
        }

        [Fact]
        public void AppendLine_ShouldNotOverwriteExistingContent()
        {
            // Arrange
            var fileSaver = new FileSaver(testFilePath);
            string initialContent = "Existing Content";
            string lineToAppend = "New Line";
            string expectedContent = initialContent + Environment.NewLine + lineToAppend + Environment.NewLine;

            try
            {
                // Create the file with initial content
                File.WriteAllText(testFilePath, initialContent + Environment.NewLine);

                // Act
                fileSaver.AppendLine(lineToAppend);
                string fileContent = File.ReadAllText(testFilePath);

                // Assert
                Assert.Equal(expectedContent, fileContent);
            }
            finally
            {
                // Clean up
                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);
                }
            }
        }
    }

    // Mock implementation of FileSaver (if not already available in the project)
    public class FileSaver
    {
        private readonly string filePath;

        public FileSaver(string filePath)
        {
            this.filePath = filePath;
        }

        public void AppendLine(string line)
        {
            using (var writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(line);
            }
        }
    }
}