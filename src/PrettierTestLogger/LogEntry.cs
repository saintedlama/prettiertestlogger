using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace PrettierTestLogger
{
    public class LogEntry
    {
        public string TestClass { get; set; }
        public string Test { get; set; }
        public TestOutcome Outcome { get; set; }
        public int DurationMs { get; set; }
    }
}