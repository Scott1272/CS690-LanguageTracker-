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

        public ConsoleUI(DataManager dataManager)
        {
            this.dataManager = dataManager;

            Tasks = new List<string>
            {
                "Enter New Word",
                "Update Comprehension Score",
            };
        }

        // Method to display the user interface and handle user input
        public void Show()
        {
            // Prompt user to select a mode
            var mode = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please select the user (Mateo OR View Mateo's Vocabulary Report): ")
                    .AddChoices(new[] { "Mateo", "View Vocabulary Report" }));
            Console.WriteLine("Welcome " + mode + " to your Language Tracker");

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

                    Console.WriteLine("WELCOME " + newWord + " To your Language Tracker");

                    // Check if the word already exists in the file
                    if (dataManager.WordExists(newWord))
                    {
                        Console.WriteLine("The word you entered already exists in the file.");

                        // Update comprehension score and timestamp for existing word
                        string comprehensionLevel = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select the level of Comprehension:")
                                .AddChoices(new[] {
                                    "Just Started", "Still learning", "Very Fluent",
                                }));

                        Console.WriteLine("Your current rating for that word is " + comprehensionLevel);

                        int comprehensionScore = comprehensionLevel switch
                        {
                            "Just Started" => 1,
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
                        Console.WriteLine("The word you entered does not exist in the file.");

                        // Ask for the comprehension score and parse it to an integer
                        string comprehensionLevel = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("Please select the level of Comprehension:")
                                .AddChoices(new[] {
                                    "Just Started", "Still learning", "Very Fluent",
                                }));

                        int comprehensionScore = comprehensionLevel switch
                        {
                            "Just Started" => 1,
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
                            .Title("What would you like to do next (Continue Entering Information OR Quit Program and take a break): ")
                            .AddChoices(new[] { "Continue", "Quit" }));

                } while (command != "Quit"); // Continue until the user types "Quit"
            }
            else if (mode == "View Vocabulary Report")
            {
                // Create a table to display the vocabulary report
                var table = new Table();

                table.AddColumn("Word");
                table.AddColumn("Comprehension Score");
                table.AddColumn("Date");

                // Retrieve all collected words
                List<string> collectedWords = dataManager.GetAllWords();
                Console.WriteLine("Collected Words:");
                foreach (var word in collectedWords)
                {
                    var parts = word.Split(':');
                    if (parts.Length == 3)
                    {
                        table.AddRow(parts[0], parts[1], parts[2]);
                    }
                }

                // Summary of learned words
                int justStartedCount = collectedWords.Count(w => w.Contains(":1:"));
                int stillLearningCount = collectedWords.Count(w => w.Contains(":2:"));
                int veryFluentCount = collectedWords.Count(w => w.Contains(":3:"));
                int totalWords = collectedWords.Count;

                Console.WriteLine($"You have learned {totalWords} words.");
                Console.WriteLine($"Comprehension Levels: Just Started: {justStartedCount}, Still Learning: {stillLearningCount}, Very Fluent: {veryFluentCount}");

                // Display the table
                AnsiConsole.Write(table);
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
