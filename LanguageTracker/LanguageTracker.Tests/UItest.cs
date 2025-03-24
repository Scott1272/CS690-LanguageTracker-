using System;
using System.IO;
using System.Collections.Generic;

namespace LanguageTracker.Tests
{
    public class UITest
    {
        private const string TestFilePath = "TestSpanishVocab.data.txt";

        public void SetUp()
        {
            // Ensure the test file is clean before each test
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        public void TestEnterNewWord()
        {
            // Arrange
            var dataManager = new DataManager(TestFilePath);
            var consoleUI = new ConsoleUI(dataManager);
            string newWord = "Rainbow";
            int comprehensionScore = 2;
            string timestamp = DateTime.Now.ToString("MM/dd/yyyy");

            // Simulate user input
            var input = new Queue<string>(new[] { "Mateo", "Enter New Word", newWord, "Still learning", "End" });
            Console.SetIn(new StringReader(string.Join(Environment.NewLine, input)));

            // Act
            consoleUI.Show();

            // Assert
            if (!dataManager.WordExists(newWord))
            {
                throw new Exception("Word does not exist.");
            }
            if (comprehensionScore != dataManager.GetComprehensionScore(newWord))
            {
                throw new Exception("Comprehension score does not match.");
            }
            if (timestamp != dataManager.GetTimestamp(newWord))
            {
                throw new Exception("Timestamp does not match.");
            }
        }
    }
}