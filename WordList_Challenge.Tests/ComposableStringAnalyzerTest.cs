namespace WordList_Challenge.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using WordList_Challenge.Analizers;
    using WordList_Challenge.Readers;
    using Xunit;
    public class ComposableStringAnalyzerTest
    {
        AnalysisResult analysisResult;
        public ComposableStringAnalyzerTest()
        {
            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IReader, MemoryReader>()
                    .AddSingleton<IAnalyzer, ComposableStringAnalyzer>()
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
