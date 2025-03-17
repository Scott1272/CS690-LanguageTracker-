using System;
using System.IO;
using Spectre.Console;

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
            
                    var mode = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("Please select the user (Mateo OR Other): ")
                        .AddChoices(new[] {
                            "Mateo", "Other",
                        }));

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
                    // Possible Delete int ComprehensionScore = int.Parse(AskForInput("Enter Comprehension Score: "));

                    string comprehensionLevel = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Please select the level of Comprehension:")
                            .AddChoices(new[] {
                                "Low Comprehension", "Still learning", "Very Fluent",
                            }));

                    int ComprehensionScore = comprehensionLevel switch
                    {
                        "Low Comprehension" => 1,
                        "Still learning" => 2,
                        "Very Fluent" => 3,
                        _ => 0
                    };


                    // Append the new word and score to the file
                    fileSaver.AppendLine(NewWord + ":" + ComprehensionScore);

                    // Ask for the next command from the user
                    
                    command = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .Title("What would you like to do next (Continue OR End): ")
                        .AddChoices(new[] {
                            "Continue", "End",
                        }));

                } while (command != "End"); // Continue until the user types "end"
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