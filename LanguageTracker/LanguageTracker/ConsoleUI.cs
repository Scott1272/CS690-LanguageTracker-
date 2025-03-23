using System;
using System.Collections.Generic;
using System.IO;
using Spectre.Console;

namespace LanguageTracker
{
    public class ConsoleUI
    {
        DataManager dataManager;
        List<string> Tasks;

        public ConsoleUI()
        {
            dataManager = new DataManager("SpanishVocab.data.txt");

            Tasks = new List<string>
            {
                "Enter New Word",
                "Update Comprehension Score",
                "View Report"
            };
        }

        // Method to display the user interface and handle user input
        public void Show()
        {
            // Prompt user to select a mode
            var mode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select the user (Mateo OR Other): ")
                    .AddChoices(new[] { "Mateo", "Report Generator" }));
            Console.WriteLine("You are now signed in as " + mode);

            // Check if the selected mode is "Mateo"
            if (mode == "Mateo")
            {
                string command = "Continue";

                // Loop to continuously ask for new words and comprehension scores
                do
                {
                    var selectedTask = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("Select a Task: ")
                            .AddChoices(Tasks));
                    Console.WriteLine("You have selected to " + selectedTask);

                    // Ask for a new word from the user
                    string newWord = AskForInput("Enter new word: ");

                    Console.WriteLine("You have entered the word " + newWord);

                    // Check if the word already exists in the file
                    if (dataManager.WordExists(newWord))
                    {
                        Console.WriteLine("The word already exists in the file.");

                        // Update comprehension score and timestamp for existing word
                        string comprehensionLevel = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select the level of Comprehension:")
                                .AddChoices(new[] {
                                    "Low Comprehension", "Still learning", "Very Fluent",
                                }));

                        Console.WriteLine("Your current rating for that word is " + comprehensionLevel);

                        int comprehensionScore = comprehensionLevel switch
                        {
                            "Low Comprehension" => 1,
                            "Still learning" => 2,
                            "Very Fluent" => 3,
                            _ => 0
                        };

                        Console.WriteLine("Based on your rating of that word your score is a " + comprehensionScore);

                        // Get the current timestamp
                        string timestamp = DateTime.Now.ToString("MM/dd/yyyy");

                        // Update the existing word's score and timestamp
                        dataManager.UpdateWord(newWord, comprehensionScore, timestamp);
                    }
                    else
                    {
                        Console.WriteLine("The word does not exist in the file.");

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
                        dataManager.AppendLine(newWord + ":" + comprehensionScore + ":" + timestamp);
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