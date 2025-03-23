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

        public bool WordExists(string word)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            var lines = File.ReadAllLines(filePath);
            return lines.Any(line => line.StartsWith(word + ":"));
        }

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

        public void AppendLine(string line)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(line);
            }
        }

        public int GetComprehensionScore(string word)
        {
            if (!File.Exists(filePath))
            {
                return 0;
            }

            var lines = File.ReadAllLines(filePath);
            var line = lines.FirstOrDefault(l => l.StartsWith(word + ":"));
            if (line != null)
            {
                var parts = line.Split(':');
                if (parts.Length == 3 && int.TryParse(parts[1], out int score))
                {
                    return score;
                }
            }

            return 0;
        }

        public string GetTimestamp(string word)
        {
            if (!File.Exists(filePath))
            {
                return string.Empty;
            }

            var lines = File.ReadAllLines(filePath);
            var line = lines.FirstOrDefault(l => l.StartsWith(word + ":"));
            if (line != null)
            {
                var parts = line.Split(':');
                if (parts.Length == 3)
                {
                    return parts[2];
                }
            }

            return string.Empty;
        }

        public List<string> GetAllWords()
        {
            if (!File.Exists(filePath))
            {
                return new List<string>();
            }

            return File.ReadAllLines(filePath).ToList();
        }
    }
}