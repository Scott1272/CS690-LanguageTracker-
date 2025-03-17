using System;
using System.IO;

namespace LanguageTracker
{
    public class ConsoleUI
    {
        FileSaver fileSaver;

        public ConsoleUI()
        {
            fileSaver = new FileSaver("SpanishVocab.data.txt");
        }

        // Method to display the user interface and handle user input
        public void Show()
        {
            // Prompt user to select a mode
            string mode = AskForInput("Please Select a user (Mateo or Other): ");

            // Check if the selected mode is "Mateo"
            if (mode == "Mateo")
            {
                string command;

                // Loop to continuously ask for new words and comprehension scores
                do
                {
                    // Ask for a new word from the user
                    string NewWord = AskForInput("Enter new word: ");

                    // Ask for the comprehension score and parse it to an integer
                    int ComprehensionScore = int.Parse(AskForInput("Enter Comprehension Score: "));

                    // Append the new word and score to the file
                    fileSaver.AppendLine(NewWord + ":" + ComprehensionScore);

                    // Ask for the next command from the user
                    command = AskForInput("Enter command (end or continue): ");

                } while (command != "end"); // Continue until the user types "end"
            }
        }

        // Static method to prompt the user for input and return the response
        public static string AskForInput(string message)
        {
            Console.Write(message); // Display the message
            return Console.ReadLine(); // Read and return user input
        }
    }
}