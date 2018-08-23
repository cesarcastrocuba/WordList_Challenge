namespace WordList_Challenge.Readers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using WordList_Challenge.Extensions;
    public class FileReader : IReader
    {
        private string filePath = "";
        public FileReader(string filePath = "wordlist.txt")
        {
            this.filePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            this.filePath = System.IO.Path.GetDirectoryName(this.filePath);
            this.filePath = string.Format("{0}\\{1}", this.filePath, filePath);
        }
        public IEnumerable<string> ReadInput()
        {
            string[] result;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            else
            {
                try
                {
                    result = File.ReadAllLines(filePath).ToArray<string>();
                }
                catch (Exception ex)
                {
                    ex.Log("");
                    throw;
                }
            }

            return result;
        }
    }

    public class MemoryReader : IReader
    {
        public IEnumerable<string> ReadInput()
        {
            string[] result = { "al", "bums", "albums",
                                "bar" , "ely" , "barely",
                                "be" , "foul" , "befoul",
                                "con" , "vex" , "convex",
                                "here" , "by" , "hereby",
                                "jig" , "saw" , "jigsaw",
                                "tail" , "or" , "tailor",
                                "we" , "aver" , "weaver"
            };

            return result;
        }
    }
}
