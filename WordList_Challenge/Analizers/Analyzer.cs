namespace WordList_Challenge.Analizers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WordList_Challenge.Extensions;
    using WordList_Challenge.Readers;

    /// <summary>
    /// The Algorithm has been extracted from 
    /// https://github.com/MikeHanson/StringFilterKata/blob/master/StringFilters/LinqStringFilter.cs
    /// As the author said it is the worst algorithm - inefficient 
    /// </summary>
    public class LinqAnalyzer : IAnalyzer
    {
        private IReader reader;
        public LinqAnalyzer(IReader reader)
        {
            this.reader = reader;
        }
        public AnalysisResult Analyze()
        {
            AnalysisResult analysisResult = new AnalysisResult();
            try
            {
                var recordLines = reader.ReadInput();

                var sourceArray = recordLines.ToArray();

                var query = from outer in sourceArray
                            from inner in sourceArray
                            where outer.Length < 6 && inner.Length < 6
                            select new { outer = outer, inner = inner };

                analysisResult.wordList =
                    from pair in query
                       let paired = pair.outer + pair.inner
                       from candidate in sourceArray
                       where candidate.Length == 6 && candidate == paired
                       select new Result() { Prefix = pair.outer, Postfix = pair.inner };
            }
            catch (Exception ex)
            {
                ex.Log("");
                throw;
            }

            return analysisResult;
        }
    }

    /// <summary>
    /// The Algorithm has been extracted from 
    /// https://github.com/MikeHanson/StringFilterKata/blob/master/StringFilters/PotentialsFirstStringFilter.cs
    /// It's better than the first one but continue been inneficient
    /// </summary>
    public class PotentialsFirstStringAnalyzer : IAnalyzer
    {
        private IReader reader;
        public PotentialsFirstStringAnalyzer(IReader reader)
        {
            this.reader = reader;
        }
        public AnalysisResult Analyze()
        {
            AnalysisResult analysisResult = new AnalysisResult();
            try
            {
                var potentials = new HashSet<Result>();
                var results = new HashSet<Result>();
                var sourceArray = reader.ReadInput().ToArray();

                foreach (var outer in sourceArray)
                {
                    if (outer.Length >= 6)
                    {
                        continue;
                    }

                    foreach (var inner in sourceArray)
                    {
                        if (inner.Length >= 6 || outer.Equals(inner) || (outer.Length + inner.Length) != 6)
                        {
                            continue;
                        }

                        potentials.Add(new Result() { Prefix = outer, Postfix = inner });
                    }
                }

                foreach (var item in sourceArray)
                {
                    if (item.Length != 6)
                    {
                        continue;
                    }

                    foreach (var potential in potentials)
                    {
                        if (item.Equals(potential.Concat))
                        {
                            results.Add(potential);
                        }
                    }
                }

                analysisResult.wordList = results;
            }
            catch (Exception ex)
            {
                ex.Log("");
                throw;
            }

            return analysisResult;
        }
    }

    /// <summary>
    /// The Algorithm has been extracted from 
    /// https://github.com/leonfs/InterviewTest--FilteringWords
    /// The best one - firstly it filters the six digits  reducing the iterations 
    /// </summary>
    public class ComposableStringAnalyzer : IAnalyzer
    {
        private IReader reader;
        public ComposableStringAnalyzer(IReader reader)
        {
            this.reader = reader;
        }
        public AnalysisResult Analyze()
        {
            AnalysisResult analysisResult = new AnalysisResult();
            try
            {
                var _stringLength = 6;

                IEnumerable<string> stringsToFilter = reader.ReadInput();
                var composableStrings = new HashSet<string>();
                foreach (var stringToFilter in stringsToFilter.Where(stringToFilter => stringToFilter.Length < _stringLength))
                {
                    composableStrings.Add(stringToFilter);
                }

                var filteredStrings = new HashSet<Result>();
                foreach (string stringToFilter in stringsToFilter)
                {
                    if (stringToFilter.Length != _stringLength)
                        continue;
                    
                    foreach (string startingSmallString in composableStrings)
                    {
                        if (stringToFilter.StartsWith(startingSmallString))
                        {
                            var stringToFilterEnding = stringToFilter.Remove(0, startingSmallString.Length);

                            if (composableStrings.Contains(stringToFilterEnding))
                                filteredStrings.Add(new Result() { Prefix = startingSmallString, Postfix = stringToFilterEnding });
                        }
                    }
                }

                analysisResult.wordList = filteredStrings;
            }
            catch (Exception ex)
            {
                ex.Log("");
                throw;
            }

            return analysisResult;
        }
    }

}
