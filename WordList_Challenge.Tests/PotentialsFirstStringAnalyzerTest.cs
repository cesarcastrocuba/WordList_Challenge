namespace WordList_Challenge.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using WordList_Challenge.Analizers;
    using WordList_Challenge.Readers;
    using Xunit;
    public class PotentialsFirstStringAnalyzerTest
    {
        AnalysisResult analysisResult;
        public PotentialsFirstStringAnalyzerTest()
        {
            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IReader, MemoryReader>()
                    .AddSingleton<IAnalyzer, PotentialsFirstStringAnalyzer>()
                    .BuildServiceProvider();

            analysisResult = serviceProvider.GetService<IAnalyzer>().Analyze();

        }
        [Fact]
        public void ShouldReturnEightElements()
        {
            Assert.Equal(8, analysisResult.wordList.Count());
        }
    }
}
