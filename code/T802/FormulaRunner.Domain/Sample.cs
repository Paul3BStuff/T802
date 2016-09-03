using System;
using System.Linq;
using stats = TextStatistics.Net;

namespace FormulaRunner.Domain
{
    /// <summary>
    /// Manages a sample.
    /// </summary>
    /// <remarks>
    /// A sample is the text of a program. 
    /// The sample also exposes properties of the text.
    /// </remarks>
    public class Sample
    {
        private readonly stats.TextStatistics _statistics;

        /// <param name="title">Identifies the sample</param>
        /// <param name="changeIdentifier">Identifies the change made to the text</param>
        /// <param name="text">The sample</param>
        public Sample(string title, string changeIdentifier, string text)
        {
            _statistics = stats.TextStatistics.Parse(text);
            Title = title;
            Text = text;
            Variant = changeIdentifier;
        }

        /// <summary>
        /// Title of the sample.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// The variant of the sample.
        /// </summary>
        public string Variant { get; private set; }
        /// <summary>
        /// Text samples text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Executes the formula on the text containes in the samples instance.
        /// </summary>
        /// <param name="formula"></param>
        /// <returns>The score achieved by the sample.</returns>
        public double Execute(IFormula formula)
        {
            var score = formula.Evaluate(Text);
            return score;
        }

        public int WordCount => _statistics.WordCount;
        public double LetterCount => _statistics.LetterCount;
        public double ComplexWords => _statistics.WordsWithThreeSyllables;
        public double AverageSyllablesPerWord => Math.Round(_statistics.AverageSyllablesPerWord, 2);

        /// <summary>
        /// Returns a count of the sentences in the Text.
        /// </summary>
        private int GetSentenceCount()
        {
            var sentences = _statistics.SentenceCount;
            return sentences;
        }

        /// <summary>
        /// Returns the number of words in the sample.
        /// A word is a collection of characters seperated by spaces.
        /// </summary>
        private int GetWordCount()
        {
            var words = _statistics.WordCount;
            return words;
        }
        
        /// <summary>
        /// The total characters in the sample.
        /// </summary>
        private int GetCharacterCount()
        {
            return Text.ToCharArray().Count();
        }

        /// <summary>
        /// Returns the total count of Syllables in the sample.
        /// </summary>
        public int GetSyllableCount()
        {
            var count = 0;
            GetWords(Text).ToList().ForEach((word) => count += GetSyllableCount(word));
            return count;
        }

        /// <summary>
        /// The total number of alpha-numeric characters in the sample.
        /// </summary>
        private int GetAlphaNumericCharacterCount()
        {
            return Text.ToCharArray().Where((chr) => char.IsLetterOrDigit(chr)).Count();
        }

        /// <summary>
        /// The average number of characters per sentence.
        /// </summary>
        /// <remarks>
        /// Does not include none alpha-numeric characters. 
        /// </remarks>
        private double GetAverageCharCountPerSentence()
        {
            double charCount;
            var sentences = GetSentences();
            if (GetSentenceCount() == 0)
            {
                charCount = 0.0;
            }
            else
            {
                charCount = sentences.Select((sentence) =>
                {
                    var chars = sentence.ToCharArray().Where((c) => char.IsLetterOrDigit(c)).Count();
                    return chars;
                }).Average();
            }
            return charCount;
        }

        /// <summary>
        /// THe average number of characters per word.
        /// </summary>
        private double GetAverageCharCountPerWord()
        {
            var words = GetWords(Text);
            var letterCount = words.Select((word) => word.Length);
            return letterCount.Count() > 0 ? letterCount.Average() : 0.0;
        }

        
        private string[] GetSentences()
        {
            return Text.Split(new[] { '.', '!', '?' }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        private int GetSyllableCount(string word)
        {
            return stats.TextStatistics.SyllableCount(word);
        }

        private string[] GetWords(string text)
        {
            var chars = text.Where(chr => char.IsSymbol(chr) || char.IsWhiteSpace(chr)).ToArray();
            var words = new string(chars);
            var wordsInText = words.Split(new[] { ' ' }).ToArray();
            return wordsInText;
        }

        public override string ToString() => $"{Title}:{Variant}";
    }
}
