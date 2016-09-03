using System.Linq;

namespace FormulaRunner.Domain
{
    /// <summary>
    /// Records information about each word in the sample.
    /// </summary>
    /// <remarks>
    /// There is one instance of this class for each word in the sample.
    /// </remarks>
    public class Word
    {
        /// <summary>
        /// Construct a new instance of a word object
        /// </summary>
        /// <param name="word">The word this instance represents</param>
        public Word(string word)
        {
            Content = word;
        }

        public string Content { get; private set; }

        public int SyllableCount
        {
            get
            {
                if (Content.All(chr => char.IsDigit(chr)))
                {
                    return 0;
                }
                return TextStatistics.Net.TextStatistics.SyllableCount(Content);
            }
        }

        /// <summary>
        /// Returns a count of Alphanumeric characters in the word (WANCC)
        /// </summary>
        public int AlphaNumericCharacterCount
        {
            get
            {
                return Content.Where((chr) => char.IsLetterOrDigit(chr)).Count();
            }
        }

        /// <summary>
        /// Returns a count of characters in the word (WCC).
        /// </summary>
        public int CharacterCount
        {
            get
            {
                return Content.ToCharArray().Count();
            }
        }

        public override string ToString() => Content;
    }
}
