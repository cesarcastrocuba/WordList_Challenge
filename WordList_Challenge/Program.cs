namespace WordList_Challenge
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using WordList_Challenge.Analizers;
    using WordList_Challenge.Dumpers;
    using WordList_Challenge.Extensions;
    using WordList_Challenge.Readers;
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //setup our DI
                var serviceProvider = new ServiceCollection()
                    //.AddSingleton<IReader, MemoryReader>()
                    .AddSingleton<IReader, FileReader>(f => new FileReader("wordlist.txt"))
                    .AddSingleton<IAnalyzer, ComposableStringAnalyzer>()
                    .BuildServiceProvider();

                AnalysisResult analysisResult = serviceProvider.GetService<IAnalyzer>().Analyze();

                //The result is stored in the bin folder in resultlist.txt
                var dumperServiceProvider = new ServiceCollection()
                    //.AddSingleton<IDumper, ConsoleDumper>(c => new ConsoleDumper(analysisResult))
                    .AddSingleton<IDumper, FileDumper>(c => new FileDumper(analysisResult, "resultlist.txt"))
                    .BuildServiceProvider();

                IDumper fileDumper = dumperServiceProvider.GetService<IDumper>();
                bool operationFinished = fileDumper.Dump();
                if (operationFinished)
                {
                    //Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Something went wrong.");
                }
            }
            catch (Exception ex)
            {
                ex.Log("");
            }
        }
    }
}
