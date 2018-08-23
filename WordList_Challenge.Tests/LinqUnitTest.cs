namespace WordList_Challenge.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using WordList_Challenge.Analizers;
    using WordList_Challenge.Readers;
    using Xunit;
    public class LinqAnalizerUnitTest
    {
        AnalysisResult analysisResult;
        public LinqAnalizerUnitTest()
        {
            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IReader, MemoryReader>()
                    .AddSingleton<IAnalyzer, LinqAnalyzer>()
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
