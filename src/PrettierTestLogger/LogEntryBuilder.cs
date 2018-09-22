using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace PrettierTestLogger
{
    public class LogEntryBuilder
    {
        public LogEntry BuildLogEntry(TestResult testResult)
        {
            var testResultDisplayNameParts = testResult.DisplayName.Split('.').ToList();

            // Take the last two parts
            var test = testResultDisplayNameParts[testResultDisplayNameParts.Count - 1];
            var testClass = testResultDisplayNameParts[testResultDisplayNameParts.Count - 2];


            return new LogEntry
            {
                TestClass = Format(testClass),
                Test = Format(test),
                Outcome = testResult.Outcome,
                DurationMs = (int)testResult.Duration.TotalMilliseconds,
            };
        }

        private string Format(string test)
        {
            if (test.Contains('_'))
            {
                return String.Join(" ", test.Split('_').Select(FirstLetterToLower));
            }

            return Regex.Replace(test, "[A-Z]", (match) => " " + FirstLetterToLower(match.Value), RegexOptions.Compiled)
                .TrimStart();
        }

        private static string FirstLetterToLower(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return text;
            }

            return Char.ToLowerInvariant(text[0]) + text.Substring(1);
        }
    }
}