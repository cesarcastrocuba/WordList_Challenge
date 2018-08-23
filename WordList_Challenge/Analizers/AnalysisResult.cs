namespace WordList_Challenge.Analizers
{
    using System.Collections.Generic;
    public class Result
    {
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public string Concat { get { return Prefix +  Postfix; } }
    }
    public class AnalysisResult
    {
        public IEnumerable<Result> wordList { get; set; }
        public AnalysisResult()
        {
            wordList = new HashSet<Result>();
        }
    }
}
