using FormulaRunner.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FormulaRunner.OutputWriters
{
    /// <summary>
    /// All Results in a single file
    /// </summary>
    /// <remarks>
    /// Writes all results (summary, sentence and word) to a single file.
    /// </remarks>
    class CumulativeResultsWriter : IResultsWriter
    {
        private readonly string _fileName;
        private readonly IStatusDisplay _statusDisplay;

        public CumulativeResultsWriter(string location, IStatusDisplay status)
        {
             _fileName = Path.Combine(location, "cumulative.csv");
            _statusDisplay = status;
        }

        public void Write(List<TestResult> results)
        {
            _statusDisplay.SetText("Writing cumulative results");
             using (var writer = new StreamWriter(_fileName))
            {
                writer.WriteLine(@"#~F~T~V~S~TSC~TWC~TSyC~TCC~TANCC~TAWS~TACCSW~TACCW~LC~CW~ASW~SSyC~SWC~SCC~SANCC~LN~ST~WT~WSy~WCC~WANCC");
                var line = new StringBuilder();
                var rowCount = results.Sum(rw => rw.Sentences.Sum(w => w.Words.Count));
                _statusDisplay.SetTotal(rowCount);
                var cursorX = Console.CursorLeft;
                var cursorY = Console.CursorTop;
                var row = 1;

                results.ForEach(formula =>
                {
                    formula.Sentences.ForEach(sentence =>
                    {
                        sentence.Words.ForEach(word =>
                        {
                            _statusDisplay.UpdateCount(row);
                            line.Append($"{row}~");
                            line.Append($"\"{formula.FormulaName}\"~"); // F
                            line.Append($"\"{formula.Title}\"~"); // T
                            line.Append($"\"{ formula.Sample.Variant}\"~"); // V
                            line.Append($"{formula.Score}~"); // S
                            line.Append($"{formula.Sentences.Count}~"); //TSC
                            line.Append($"{formula.WordCount}~"); // TWC
                            line.Append($"{formula.SyllableCount}~"); // TSyC
                            line.Append($"{formula.CharacterCount}~"); // TCC
                            line.Append($"{formula.AlphaNumericCharacterCount}~"); // TANCC
                            line.Append($"{formula.AverageWordsPerSentence:0.00}~"); // TAWS 
                            line.Append($"{formula.AverageCharCountPerSentenceWord:0.00}~"); // TACCSW
                            line.Append($"{formula.AverageCharCountPerWord:0.00}~"); // TACCW
                            line.Append($"{formula.Sample.LetterCount}~"); // LC
                            line.Append($"{formula.Sample.ComplexWords}~"); // CW
                            line.Append($"{formula.Sample.AverageSyllablesPerWord:0.00}~"); // ASW

                            line.Append($"{sentence.SyllableCount}~"); // SSyC
                            line.Append($"{sentence.Words.Count}~"); // SWC
                            line.Append($"{sentence.CharacterCount}~"); // SCC
                            line.Append($"{sentence.AlphaNumericCharacterCount}~"); // SANCC
                            line.Append($"\"{sentence.LineNumber}\"~"); // LN
                            line.Append($"\"{sentence.Content}\"~"); // ST

                            line.Append($"\"{word.Content}\"~"); // WT
                            line.Append($"{word.SyllableCount}~");  // WSy
                            line.Append($"{word.CharacterCount}~"); // WCC
                            line.Append($"{word.AlphaNumericCharacterCount}~"); // WANCC

                            writer.WriteLine(line.ToString());
                            line.Clear();
                            row += 1;
                        });
                    });
                });
                writer.Flush();
                writer.Close();
            }
            Console.WriteLine("");
        }
    }
}
