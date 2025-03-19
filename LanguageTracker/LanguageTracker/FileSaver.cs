using System;
using System.IO;
using System.Linq;

namespace LanguageTracker
{
    public class FileSaver
    {
        private readonly string filePath;

        public FileSaver(string filePath)
        {
            this.filePath = filePath;
        }

        public void AppendLine(string line)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(line);
            }
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

        public void UpdateWord(string word, int comprehensionScore, string timestamp)
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
                    lines[i] = $"{word}:{comprehensionScore}:{timestamp}";
                    break;
                }
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}