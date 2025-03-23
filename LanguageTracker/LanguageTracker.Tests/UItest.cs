using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;

namespace LanguageTracker.Tests
{
    [TestFixture]
    public class UITest
    {
        private const string TestFilePath = "TestSpanishVocab.data.txt";

        [SetUp]
        public void SetUp()
        {
            // Ensure the test file is clean before each test
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [Test]
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
            Assert.IsTrue(dataManager.WordExists(newWord));
            Assert.AreEqual(comprehensionScore, dataManager.GetComprehensionScore(newWord));
            Assert.AreEqual(timestamp, dataManager.GetTimestamp(newWord));
        }
    }
}