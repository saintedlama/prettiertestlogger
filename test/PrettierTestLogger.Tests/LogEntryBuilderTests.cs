using System;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

namespace PrettierTestLogger.Tests
{
    public class LogEntryBuilderTests
    {
        [Fact]
        public void Should_Split_And_LowerCase_CamelCased_Test_Names()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.ShouldSplitAndLowerCaseCamelCasedTestNames");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("should split and lower case camel cased test names", logEntry.Test);
        }

        [Fact]
        public void Should_Split_And_Lower_Case_Dash_Separated_Test_Names()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.Should_Split_And_Lower_Case_Dash_Separated_Test_Names");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("should split and lower case dash separated test names", logEntry.Test);
        }

        [Fact]
        public void Should_Not_Split_CamelCase_If_Dash_Separated_Test_Names_Are_Used()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.Should_Not_Split_CamelCase_If_Dash_Separated_Test_Names_Are_Used");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("should not split camelCase if dash separated test names are used", logEntry.Test);
        }

        [Fact]
        public void Should_LowerCase_First_Letter_If_Dash_Is_Used()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.Should_LowerCase_First_Letter_If_Dash_Is_Used");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("should lowerCase first letter if dash is used", logEntry.Test);
        }

        [Fact]
        public void Should_Split_And_LowerCase_CamelCased_TestClass_Names()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.ShouldSplitAndLowerCaseCamelCasedTestNames");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("log entry builder tests", logEntry.TestClass);
        }

        [Fact]
        public void Should_Split_And_LowerCase_Dash_Separated_TestClass_Names()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.Log_Entry_Builder_Tests.ShouldSplitAndLowerCaseCamelCasedTestNames");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("log entry builder tests", logEntry.TestClass);
        }

        [Fact]
        public void Should_LowerCase_First_Letter_If_Dash_Is_Used_For_TestClass_Names()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.Log_EntryBuilder_Tests.ShouldSplitAndLowerCaseCamelCasedTestNames");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("log entryBuilder tests", logEntry.TestClass);
        }

        [Fact]
        public void Should_Not_Split_Abbrevations_In_Uppercase()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.ShouldNotSplitABBREVATIONS");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("should not split ABBREVATIONS", logEntry.Test);
        }

       [Fact]
        public void Should_Not_Split_Abbrevations_In_Uppercase_Within_Text()
        {
            var builder = new LogEntryBuilder();
            var testResult = BuildTestResultFromDisplayName("PrettierTestLogger.Tests.LogEntryBuilderTests.ShouldNotSplitABBREVATIONSInText");

            var logEntry = builder.BuildLogEntry(testResult);
            Assert.Equal("should not split ABBREVATIONS in text", logEntry.Test);
        }
        private TestResult BuildTestResultFromDisplayName(string displayName)
        {
            return new TestResult(new TestCase { DisplayName = displayName }) {
                DisplayName = displayName,
            };
        }
    }
}
