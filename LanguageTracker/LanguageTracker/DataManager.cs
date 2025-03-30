using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace LanguageTracker
{
    public class DataManager
    {
        private string filePath;

        public DataManager(string filePath)
        {
            this.filePath = filePath;
        }

        // Check if a word exists in the file
        public bool WordExists(string word)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            var lines = File.ReadAllLines(filePath);
            return lines.Any(line => line.StartsWith(word + ":"));
        }

        // Update the comprehension score and timestamp for an existing word
        public void UpdateWord(string word, int score, string timestamp)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            var lines = File.ReadAllLines(filePath).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith(word + ":"))
                {
                    lines[i] = $"{word}:{score}:{timestamp}";
                    break;
                }
            }

            File.WriteAllLines(filePath, lines);
        }

        // Append a new word entry to the file
        public void AppendLine(string line)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(line);
            }
        }

        // Retrieve all words from the file
        public List<string> GetAllWords()
        {
            if (!File.Exists(filePath))
            {
                return new List<string>();
            }

            return File.ReadAllLines(filePath).ToList();
        }

        // Get the total number of words in the file
        public int GetTotalWords()
        {
            if (!File.Exists(filePath))
            {
                return 0;
            }

            return File.ReadAllLines(filePath).Length;
        }

        // Get the number of words by comprehension level
        public int GetWordsByComprehensionLevel(string level)
        {
            if (!File.Exists(filePath))
            {
                return 0;
            }

            var lines = File.ReadAllLines(filePath);
            int score = level switch
            {
                "Just Started" => 1,
                "Still learning" => 2,
                "Very Fluent" => 3,
                _ => 0
            };

            return lines.Count(line =>
            {
                var parts = line.Split(':');
                return parts.Length == 3 && int.TryParse(parts[1], out int parsedScore) && parsedScore == score;
            });
        }
    }
}