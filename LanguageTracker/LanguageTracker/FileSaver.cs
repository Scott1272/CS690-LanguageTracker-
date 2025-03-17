using System;
using System.IO;

namespace LanguageTracker
{
    public class FileSaver
    {
        private string fileName;

        public FileSaver(string filename)
        {
            this.fileName = filename;
            // Ensure the file is created if it does not exist
            if (!File.Exists(this.fileName))
            {
                using (FileStream fs = File.Create(this.fileName))
                {
                    // Close the file stream to release the file handle
                }
            }
        }

        public void AppendLine(string line)
        {
            File.AppendAllText(this.fileName, line + Environment.NewLine);
        }
    }
}