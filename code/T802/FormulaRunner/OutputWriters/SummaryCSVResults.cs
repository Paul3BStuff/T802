using FormulaRunner.Domain;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FormulaRunner.OutputWriters
{
    /// <summary>
    /// Summary level results.
    /// </summary>
    /// <remarks>
    /// Writes a summary level CSV file.
    /// The summary is the results for the sample at sample level
    /// </remarks>
    class SummaryCSVResults : IResultsWriter
    {
        private readonly string _fileName;
        private readonly IStatusDisplay _statusDisplay;

        public SummaryCSVResults(string location, IStatusDisplay status)
        {
            _fileName = Path.Combine(location, "summary.csv");
            _statusDisplay = status;
        }

        public void Write(List<TestResult> results)
        {
            _statusDisplay.SetText("Writing Top level sample results");
            _statusDisplay.SetTotal(results.Count);
            using (var writer = new StreamWriter(_fileName))
            {
                var row = 1;
                writer.WriteLine("#~F~T~V~S~TSC~TWC~TSyC~TCC~TANCC~TAWS~TACCS~TACCSW~LC~CW~ASW");
                var line = new StringBuilder();
                results.ForEach(result =>
                {
                    _statusDisplay.UpdateCount(row);
                    line.Append($"{row++}~"); // #
                    line.Append($"\"{result.FormulaName}\"~"); // F
                    line.Append($"\"{result.Title}\"~"); // T
                    line.Append($"\"{result.Sample.Variant}\"~"); // V
                    line.Append($"{result.Score}~"); // S
                    line.Append($"{result.Sentences.Count}~"); // TSC
                    line.Append($"{result.WordCount}~"); // TWC
                    line.Append($"{result.SyllableCount}~"); // TSyC
                    line.Append($"{result.CharacterCount}~"); // TCC
                    line.Append($"{result.AlphaNumericCharacterCount}~"); // TANCC
                    line.Append($"{result.AverageWordsPerSentence}~"); // TAWS
                    line.Append($"{result.AverageCharCountPerSentenceWord}~"); // TACCS
                    line.Append($"{result.AverageCharCountPerWord}~"); // TACCSW
                    line.Append($"{result.Sample.LetterCount}~"); // LC
                    line.Append($"{result.Sample.ComplexWords}~"); // CW
                    line.Append($"{result.Sample.AverageSyllablesPerWord:0.00}"); // ASW
                    writer.WriteLine(line.ToString());
                    line.Clear();
                });
                writer.Flush();
                writer.Close();
            }
        }
    }
}
