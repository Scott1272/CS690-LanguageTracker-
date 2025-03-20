using System;
using System.Collections.Generic;
using System.IO;
using Spectre.Console;

namespace LanguageTracker
{
    public class ConsoleUI
    {
        FileSaver fileSaver;
        List<string> Tasks;
        
        public ConsoleUI()
        {
            fileSaver = new FileSaver("SpanishVocab.data.txt");

            Tasks = new List<string>
            {
                "Enter New Word",
                "Update Comprehension Score",
                "View Report",
                "End"
            };
        }

        // Method to display the user interface and handle user input
        public void Show()
        {
            // Prompt user to select a mode
            var mode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select the user (Mateo OR Other): ")
                    .AddChoices(new[] { "Mateo", "Other" }));

            // Check if the selected mode is "Mateo"
            if (mode == "Mateo")
            {
                var selectedTask = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select a Task: ")
                        .AddChoices(Tasks));
                Console.WriteLine("You have selected to " + selectedTask);

                string command;

                // Loop to continuously ask for new words and comprehension scores
                do
                {
                    // Ask for a new word from the user
                    string newWord = AskForInput("Enter new word: ");

                    // Check if the word already exists in the file
                    if (fileSaver.WordExists(newWord))
                    {
                        // Update comprehension score and timestamp for existing word
                        string comprehensionLevel = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select the level of Comprehension:")
                                .AddChoices(new[] {
                                    "Low Comprehension", "Still learning", "Very Fluent",
                                }));

                        int comprehensionScore = comprehensionLevel switch
                        {
                            "Low Comprehension" => 1,
                            "Still learning" => 2,
                            "Very Fluent" => 3,
                            _ => 0
                        };

                        // Get the current timestamp
                        string timestamp = DateTime.Now.ToString("MM/dd/yyyy");

                        // Update the existing word's score and timestamp
                        fileSaver.UpdateWord(newWord, comprehensionScore, timestamp);
                    }
                    else
                    {
                        // Ask for the comprehension score and parse it to an integer
                        string comprehensionLevel = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select the level of Comprehension:")
                                .AddChoices(new[] {
                                    "Low Comprehension", "Still learning", "Very Fluent",
                                }));

                        int comprehensionScore = comprehensionLevel switch
                        {
                            "Low Comprehension" => 1,
                            "Still learning" => 2,
                            "Very Fluent" => 3,
                            _ => 0
                        };

                        // Get the current timestamp
                        string timestamp = DateTime.Now.ToString("MM/dd/yyyy");

                        // Append the new word and score to the file
                        fileSaver.AppendLine(newWord + ":" + comprehensionScore + ":" + timestamp);
                    }

                    // Ask for the next command from the user
                    command = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What would you like to do next (Continue OR End): ")
                            .AddChoices(new[] { "Continue", "End" }));

                } while (command != "End"); // Continue until the user types "End"
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
