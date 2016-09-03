using System;
using System.Collections.Generic;
using System.Linq;

namespace FormulaRunner.Domain
{
    /// <summary>
    /// Represents a single sentence in the results of a sample.
    /// </summary>
    public class Sentence
    {
        public Sentence(string sentence, int number)
        {
            LineNumber = number;
            Content = sentence;
            Words = new List<Word>(ExtractWords(Content));
        }

        /// <summary>
        /// The line number of the sentence in the sample.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// Extracts a collection of words from the collection.
        /// </summary>
        /// <param name="sentence">The sentence to be processed.</param>
        /// <returns>A collection of word instances.</returns>
        private static IEnumerable<Word> ExtractWords(string sentence)
        {
            var words = sentence.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Where((word) => word != "\n")
            .Select((word) => word.Replace("'", ""))
            .Select((word) => word.Trim())
            .Select((word) => new Word(word));
            return words.AsEnumerable();
        }

        /// <summary>
        /// The sentence.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// The words in the sentence (W).
        /// </summary>
        public List<Word> Words { get; private set; }

        /// <summary>
        /// Returns the number of characters in the sentence (WCC).
        /// </summary>
        public int CharacterCount
        {
            get
            {
                var count = Content.Count();
                return count;
            }
        }

        /// <summary>
        /// Returns the number of characters in words (WANCC).
        /// </summary>
        public int AlphaNumericCharacterCount
        {
            get
            {
                var count = Words.Select((word) => word.AlphaNumericCharacterCount).DefaultIfEmpty(0).Sum();
                return count;
            }
        }

        /// <summary>
        /// Returns the count of syllables in the sentence.
        /// </summary>
        public int SyllableCount
        {
            get
            {
                return Words.Select((word) => word.SyllableCount).DefaultIfEmpty(0).Sum();
            }
        }

        public override string ToString() => $"{LineNumber}:{Content}";
    }
}
