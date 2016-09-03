using FormulaRunner.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FormulaRunner.OutputWriters
{
    /// <summary>
    /// Sentence level results
    /// </summary>
    /// <remarks>
    /// Writes results at the level of sentences.
    /// </remarks>
    class SentencesCSVResultsWriter : IResultsWriter
    {
        private readonly string _fileName;
        private readonly IStatusDisplay _statusDisplay;

        public SentencesCSVResultsWriter(string location, IStatusDisplay status)
        {
            _fileName = Path.Combine(location, "sentences.csv");
            _statusDisplay = status;
        }

        public void Write(List<TestResult> results)
        {
            _statusDisplay.SetText("Writing sentence results");
            using (var writer = new StreamWriter(_fileName))
            {
                var row = 1;
                var line = new StringBuilder();
                _statusDisplay.SetTotal(results.Select(sentence => sentence.Sentences.Count()).Sum());
                writer.WriteLine("#~F~T~V~SWC~TSyC~SSyC~SCC~SANCC~LN~ST");
                results.ForEach(result =>
                {
                    result.Sentences.ForEach(sentence =>
                    {
                        _statusDisplay.UpdateCount(row);
                        line.Append($"{row}~"); // #
                        line.Append($"\"{result.FormulaName}\"~"); // F
                        line.Append($"\"{result.Title}\"~"); // T
                        line.Append($"\"{result.Sample.Variant}\"~"); // V
                        line.Append($"{sentence.Words.Count}~"); //SWC
                        line.Append($"{result.SyllableCount}~"); // TSyC
                        line.Append($"{sentence.SyllableCount}~"); // SSyC
                        line.Append($"{sentence.CharacterCount}~"); // SCC
                        line.Append($"{sentence.AlphaNumericCharacterCount}~"); // SANCC
                        line.Append($"\"{sentence.LineNumber}\"~"); // LN
                        line.Append($"\"{sentence.Content}\""); // ST
                        writer.WriteLine(line);
                        line.Clear();
                        row += 1;
                    });
                });
                writer.Flush();
                writer.Close();
            }
        }
    }
}
