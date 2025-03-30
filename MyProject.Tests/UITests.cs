using System;
using System.Collections.Generic;
using Xunit;

public class UITests
{
    [Fact]
    public void GenerateReport_ShouldDisplayCorrectTableFormat()
    {
        // Arrange
        var wordDataList = new List<WordData>
        {
            new WordData { Word = "Hello", ComprehensionLevel = "High", ComprehensionScore = 90, Date = new DateTime(2023, 10, 1) },
            new WordData { Word = "World", ComprehensionLevel = "Medium", ComprehensionScore = 75, Date = new DateTime(2023, 10, 2) }
        };

        var reportGenerator = new ReportGenerator();

        // Act
        using (var consoleOutput = new ConsoleOutput())
        {
            reportGenerator.GenerateReport(wordDataList);
            var output = consoleOutput.GetOutput();

            // Assert
            Assert.Contains("Word\t\tComprehension Level\tComprehension Score\tDate", output);
            Assert.Contains("Hello\t\tHigh\t\t90\t\t2023-10-01", output);
            Assert.Contains("World\t\tMedium\t\t75\t\t2023-10-02", output);
        }
    }
}

// Helper class to capture console output
public class ConsoleOutput : IDisposable
{
    private readonly System.IO.StringWriter _stringWriter;
    private readonly System.IO.TextWriter _originalOutput;

    public ConsoleOutput()
    {
        _stringWriter = new System.IO.StringWriter();
        _originalOutput = Console.Out;
        Console.SetOut(_stringWriter);
    }

    public string GetOutput()
    {
        return _stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(_originalOutput);
        _stringWriter.Dispose();
    }
}

// Mock class for WordData (if not already available in the project)
public class WordData
{
    public string Word { get; set; }
    public string ComprehensionLevel { get; set; }
    public int ComprehensionScore { get; set; }
    public DateTime Date { get; set; }
}

// Mock class for ReportGenerator (if not already available in the project)
public class ReportGenerator
{
    public void GenerateReport(List<WordData> wordDataList)
    {
        Console.WriteLine("Word\t\tComprehension Level\tComprehension Score\tDate");
        Console.WriteLine("-------------------------------------------------------------");

        foreach (var wordData in wordDataList)
        {
            Console.WriteLine($"{wordData.Word}\t\t{wordData.ComprehensionLevel}\t\t{wordData.ComprehensionScore}\t\t{wordData.Date:yyyy-MM-dd}");
        }
    }
}
