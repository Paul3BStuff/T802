using FormulaRunner.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FormulaRunner.OutputWriters
{
    /// <summary>
    /// Writes a basic CSV format file.
    /// </summary>
    /// <remarks>
    /// Writes result at the level of individual words.
    /// </remarks>
    class WordsCSVResults : IResultsWriter
    {
        private readonly string _fileName;
        private readonly IStatusDisplay _statusDisplay;

        public WordsCSVResults(string location, IStatusDisplay status)
        {
            _fileName = Path.Combine(location, "words.csv");
            _statusDisplay = status;
        }

        public void Write(List<TestResult> results)
        {
            _statusDisplay.SetText("Writing words results");
            _statusDisplay.SetTotal(results.Count);

            using (var writer = new StreamWriter(_fileName))
            {
                var row = 1;
                var line = new StringBuilder();
                writer.WriteLine("#~F~T~V~W~WSyC~WCC~WANCC");
                _statusDisplay.SetTotal(results.Select(result => result.WordCount).Sum());
                results.ForEach(result =>
                {
                    result.Sentences.ForEach(sentence =>
                    {
                        sentence.Words.ForEach(word =>
                        {
                            _statusDisplay.UpdateCount(row);
                            line.Append($"{row++}~"); // #
                            line.Append($"\"{result.FormulaName}\"~"); // F
                            line.Append($"\"{result.Title}\"~"); // T
                            line.Append($"\"{result.Sample.Variant}\"~"); // V
                            line.Append($"\"{word.Content}\"~"); // W
                            line.Append($"{word.SyllableCount}~"); // WSyC
                            line.Append($"{word.CharacterCount}~"); // WCC
                            line.Append($"{word.AlphaNumericCharacterCount}"); // WANCC
                            writer.WriteLine(line);
                            line.Clear();
                        });
                    });
                });
                writer.Flush();
                writer.Close();
            }
        }
    }
}
