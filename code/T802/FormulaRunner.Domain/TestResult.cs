using System;
using System.Collections.Generic;
using System.Linq;

namespace FormulaRunner.Domain
{
    /// <summary>
    /// Contains the result of the execution of a test
    /// </summary>
    /// <remarks>
    /// An instance of this class is created for each text
    /// every formula.
    /// </remarks>
    public class TestResult
    {
        public TestResult()
        {
            Sentences = new List<Sentence>();
        }

        /// <summary>
        /// Constructor method to build the TestResult from a sample.
        /// </summary>
        /// <param name="sample">The sample to be processed.</param>
        /// <param name="formulaName">The formula used.</param>
        /// <param name="score">Score achieved by the sample for the formula.</param>
        /// <returns></returns>
        public static TestResult Build(Sample sample, string formulaName, double score)
        {
            var result = new TestResult
            {
                Score = score,
                Sample = sample,
                FormulaName = formulaName
            };
            var sentences = GetSentences(sample.Text);
            result.Sentences.AddRange(sentences);
            return result;
        }

        /// <summary>
        /// Returns all sentences from the sample.
        /// </summary>
        /// <param name="sample">The sample to be analysed</param>
        /// <returns>IEnumable of Sentence</returns>
        /// <remarks>A sentence is terminated by the full-stop.</remarks>
        private static IEnumerable<Sentence> GetSentences(string sample)
        {
            var sentences = sample.Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                .Select((sentence) => sentence.Trim())
                .Select((sentence, index) => new Sentence(sentence, index + 1))
                .Where((sentence) => sentence.Words.Count > 0);
            return sentences;
        }

        /// <summary>
        /// The title of the sample (T).
        /// </summary>
        public string Title { get { return Sample.Title; } }

        /// <summary>
        /// The name of the formula (F).
        /// </summary>
        public string FormulaName { get; private set; }

        /// <summary>
        /// The score achieved by the sample (S).
        /// </summary>
        public double Score { get; internal set; }

        /// <summary>
        /// Returns a count of the words in the sample (TWC).
        /// </summary>
        public int WordCount
        {
            get
            {
                var count = Sentences.Select((sentence) => sentence.Words.Count()).DefaultIfEmpty(0).Sum();
                return count;
            }
        }

        /// <summary>
        /// Returns a count of the syllables in the sample (TsyC).
        /// </summary>
        public int SyllableCount
        {
            get
            {
                return Sentences.Select((sentence) => sentence.SyllableCount).DefaultIfEmpty(0).Sum();
            }
        }

        /// <summary>
        /// Returns the average word count per sentence (TAWS).
        /// </summary>
        public double AverageWordsPerSentence
        {
            get
            {
                var avg = Sentences.Select((sentence) => sentence.Words.Count).DefaultIfEmpty(0);
                return Math.Round(avg.Average(), 2);
            }
        }

        /// <summary>
        /// Returns a count of all characters in the sample (TCC).
        /// </summary>
        /// <remarks>
        /// Includes all characters including non alpha numeric.
        /// </remarks>
        public int CharacterCount
        {
            get
            {
                var count = Sentences.Select((sentence) => sentence.CharacterCount).Sum();
                return count;
            }
        }

        /// <summary>
        /// Returns the number of characters considered to be part of a word (TANCC).
        /// </summary>
        public int AlphaNumericCharacterCount
        {
            get
            {
                var count = Sentences.Select((sentence) => sentence.AlphaNumericCharacterCount).DefaultIfEmpty(0).Sum();
                return count;
            }
        }

        /// <summary>
        /// Returns the average character count per word (TACCS).
        /// </summary>
        public double AverageCharCountPerWord
        {
            get
            {
                var avg = 0.0;
                var listOfWords = new List<Word>();
                foreach (var sentence in Sentences)
                {
                    listOfWords.AddRange(sentence.Words);
                }
                avg = listOfWords.Select((word) => word.CharacterCount).DefaultIfEmpty(0).Average();
                return Math.Round(avg, 2);
            }
        }

        /// <summary>
        /// Returns the average character count per sentence word (TACCSW).
        /// </summary>
        public double AverageCharCountPerSentenceWord
        {
            get
            {
                var avg = Sentences.Select((sentence) => sentence.AlphaNumericCharacterCount).DefaultIfEmpty(0).Average();
                return Math.Round(avg);
            }
        }

        /// <summary>
        /// Returns the sample object the results were calculated from.
        /// </summary>
        public Sample Sample { get; internal set; }

        public List<Sentence> Sentences { get; private set; }

        public override string ToString() => $"{Title}:{FormulaName}={Score}";
    }
}
