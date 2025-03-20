namespace LanguageTracker
{
    public class Task
    {
        public string Name { get; }

        public Task(string name)
        {
            this.Name = name;
        }
    }

    public class User
    {
        public string Name { get; }

        public User(string name)
        {
            this.Name = name;
        }
    }

    public class ComprehensionScore
    {
        public string Name { get; }

        public ComprehensionScore(string name)
        {
            this.Name = name;
        }
    }

    public class Word
    {
        public string Name { get; }
        public ComprehensionScore ComprehensionScore { get; }

        public Word(string name, ComprehensionScore comprehensionScore)
        {
            this.Name = name;
            this.ComprehensionScore = comprehensionScore;
        }
    }

    public class Report
    {
        public string Name { get; }
        public string Word { get; }
        public string ComprehensionScore { get; }

        public Report(string name, string word, string comprehensionScore)
        {
            this.Name = name;
            this.Word = word;
            this.ComprehensionScore = comprehensionScore;
        }
    }
}