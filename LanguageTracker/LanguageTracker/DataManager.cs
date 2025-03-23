using System;
using System.IO;
using System.Linq;

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
    }
}