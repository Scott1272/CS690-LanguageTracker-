using System;
using System.IO;
using System.Collections.Generic;
using Xunit;

namespace LanguageTracker.Tests
{
    public class UITest
    {
        private const string TestFilePath = "TestSpanishVocab.data.txt";

        // Ensure the test file is clean before each test
        public void SetUp()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [Fact]
        public void TestEnterNewWord()
        {
            // Arrange
            SetUp(); // Clean up the test file before running the test
            var dataManager = new DataManager(TestFilePath);
            var consoleUI = new ConsoleUI(dataManager);
            string newWord = "Rainbow";
            int comprehensionScore = 2; // "Still learning" corresponds to score 2
            string timestamp = DateTime.Now.ToString("MM/dd/yyyy");

            // Simulate user input
            var input = new Queue<string>(new[] { "Mateo", "Enter New Word", newWord, "Still learning", "End" });
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, input)));

            // Act
            consoleUI.Show();

            // Assert
            Assert.True(dataManager.WordExists(newWord), "Word does not exist in the data file.");
            Assert.Equal(comprehensionScore, dataManager.GetComprehensionScore(newWord));
            Assert.Equal(timestamp, dataManager.GetTimestamp(newWord));
        }
    }
}