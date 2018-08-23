namespace WordList_Challenge.Dumpers
{
    using System;
    using WordList_Challenge.Analizers;
    using WordList_Challenge.Extensions;
    public class ConsoleDumper : IDumper
    {
        private AnalysisResult analysisResult;

        public ConsoleDumper(AnalysisResult analysisResult)
        {
            this.analysisResult = analysisResult;
        }
        public bool Dump()
        {
            bool result = false;

            try
            {
                foreach (var word in analysisResult.wordList)
                {
                    Console.WriteLine(string.Format("{0} + {1} => {2}", word.Prefix, word.Postfix, word.Concat));
                }
                result = true;
            }
            catch (Exception ex)
            {
                ex.Log("");     //This extension could use, again, some different strategies depending on context.
                result = false;
            }

            return result;
        }
    }

    public class FileDumper : IDumper
    {
        private string filePath = "";
        private AnalysisResult analysisResult;
        public FileDumper(AnalysisResult analysisResult, string filePath = "resultlist.txt")
        {
            this.analysisResult = analysisResult;

            this.filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            this.filePath = System.IO.Path.GetDirectoryName(this.filePath);
            this.filePath = string.Format("{0}\\{1}", this.filePath, filePath);
        }

        public bool Dump()
        {
            bool result = false;

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.filePath))
                {
                    foreach (var word in analysisResult.wordList)
                    {
                        file.WriteLine(string.Format("{0} + {1} => {2}", word.Prefix, word.Postfix, word.Concat));
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                ex.Log("");
                result = false;
            }

            return result;
        }
    }
}
