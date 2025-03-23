using System;

namespace LanguageTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a DataManager instance with the path to the data file
            DataManager dataManager = new DataManager("SpanishVocab.data.txt");

            // Pass the DataManager instance to the ConsoleUI constructor
            ConsoleUI theUI = new ConsoleUI(dataManager);
            theUI.Show();
        }
    }
}