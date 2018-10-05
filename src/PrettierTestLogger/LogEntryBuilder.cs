using System;
using System.Collections.Generic;
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

            if (testResultDisplayNameParts.Count < 2)
            {
                // This is a best effort try if things go wrong
                return new LogEntry
                {
                    TestClass = Format(testResult.DisplayName),
                    Test = Format(testResult.DisplayName),
                    Outcome = testResult.Outcome,
                    DurationMs = (int)testResult.Duration.TotalMilliseconds,
                };
            }

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
            if (String.IsNullOrWhiteSpace(test))
            {
                return test;
            }

            if (test.Contains('_'))
            {
                return String.Join(" ", test.Split('_').Select(FirstLetterToLower));
            }

            var replacedAbbrevations = Regex.Replace(test, "[A-Z]{2,}", (match) => " " + match.Value, RegexOptions.Compiled).TrimStart();

            return Regex.Replace(replacedAbbrevations, "[A-Z][a-z]+", (match) => " " + FirstLetterToLower(match.Value), RegexOptions.Compiled).TrimStart();
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