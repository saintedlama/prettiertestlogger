using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;

namespace PrettierTestLogger
{
    [FriendlyName(FriendlyName)]
    [ExtensionUri(ExtensionUri)]
    public class TestLogger : ITestLogger
    {
        public const string ExtensionUri = "logger://Microsoft/TestPlatform/PrettierTestLogger/v1";
        public const string FriendlyName = "prettier";

        private List<LogEntry> LogEntries { get; set; }
        private LogEntryBuilder Builder { get; set; }

        public void Initialize(TestLoggerEvents events, Dictionary<string, string> parameters)
        {
            LogEntries = new List<LogEntry>();
            Builder = new LogEntryBuilder();

            events.TestResult += TestResultHandler;
            events.TestRunComplete += TestRunCompleteHandler;
        }

        public void Initialize(TestLoggerEvents events, string testRunDirectory)
        {
            LogEntries = new List<LogEntry>();
            Builder = new LogEntryBuilder();

            events.TestResult += TestResultHandler;
            events.TestRunComplete += TestRunCompleteHandler;
        }

        public void TestResultHandler(object sender, TestResultEventArgs e)
        {
            LogEntries.Add(Builder.BuildLogEntry(e.Result));
        }

        public void TestRunCompleteHandler(object sender, TestRunCompleteEventArgs e)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("TestRunCompleteHandler - Writing Test results ");

            var orderedByTestClass = new Dictionary<string, List<LogEntry>>();

            foreach (var logEntry in LogEntries)
            {
                if (!orderedByTestClass.ContainsKey(logEntry.TestClass))
                {
                    orderedByTestClass[logEntry.TestClass] = new List<LogEntry>();
                }

                orderedByTestClass[logEntry.TestClass].Add(logEntry);
            }

            var foregroundColor = Console.ForegroundColor;

            foreach (var entry in orderedByTestClass)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{entry.Key}");

                foreach (var logEntry in entry.Value)
                {
                    Console.Write("  ");
                    PrintTestOutcome(logEntry.Outcome);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(logEntry.Test);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($" ({logEntry.DurationMs}ms)");
                    Console.WriteLine();
                }
            }

            Console.ForegroundColor = foregroundColor;
        }

        public static void PrintTestOutcome(TestOutcome testOutcome)
        {
            switch (testOutcome)
            {
                case TestOutcome.Passed:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("✓ ");
                    break;
                case TestOutcome.Failed:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("✗ ");
                    break;
                case TestOutcome.None:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("- ");
                    break;
                case TestOutcome.NotFound:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("? ");
                    break;
                case TestOutcome.Skipped:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("~ ");
                    break;
                default:
                    throw new InvalidOperationException($"Test outcome {testOutcome} is not a supported test outcome");
            }
        }
    }
}