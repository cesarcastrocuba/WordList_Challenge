namespace WordList_Challenge.Readers
{
    using System.Collections.Generic;
    public interface IReader
    {
        IEnumerable<string> ReadInput();
    }
}
