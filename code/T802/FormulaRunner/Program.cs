using FormulaRunner.Domain;
using FormulaRunner.Domain.Formulas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using FormulaRunner.OutputWriters;
using static System.Console;

namespace FormulaRunner
{
    /// <summary>
    /// Entry point for the program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Method called by CLI to start execution.
        /// </summary>
        /// <param name="args">Array of arguments passed to program from the command line.</param>
        /// <remarks>
        /// Program accepts two parameters: 
        /// 1: The name and path to the samples file. e.g. c:\samples.xml
        /// 2: The location to write the output files. These are witten to a folder within the
        /// folder given in the second parameter using the name Output-ddmmyyyy-hhmmss.
        /// the format of the parameter is the path to the folder e.g. c:\results
        /// </remarks>
        static void Main(string[] args)
        {
            var inputFile = args[0];
            var location = args[1];
            WriteLine($"     Input file: {inputFile}");
            WriteLine($"Output location: {location}");
            if (ParametersAreValid(inputFile, location))
            {
                ProcessSamples(inputFile, location);
            }
        }

        /// <summary>
        /// Check if the parameters are valid.
        /// </summary>
        /// <param name="inputFile">The name of the file containing samples.</param>
        /// <param name="outputFolder">The name of the folder the results will be written to.</param>
        /// <remarks>
        /// Sets the ExitCode for the application.
        /// </remarks>
        private static bool ParametersAreValid(string inputFile, string outputFolder)
        {
            var valid = true;
            if (InputFileNotFound(inputFile))
            {
                WriteLine($"Input file: {inputFile} not found");
                Environment.ExitCode = 1;
                return false;
            }
            return valid;
        }

        /// <summary>
        /// Check if input file given in arguments exists.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true if the does not exist at the path given.</returns>
        private static bool InputFileNotFound(string path) => !File.Exists(path);

        private static List<Sample> LoadTests(string path)
        {
            var testSet = new List<Sample>();
            var document = XDocument.Load(path).Descendants();
            var tests = document.Elements("sample");
            foreach (var testDefinition in tests)
            {
                var title = testDefinition.Attribute("title").Value;
                var ci = testDefinition.Attribute("variant").Value;
                var text = testDefinition.Value;
                var test = new Sample(title, ci, text);
                testSet.Add(test);
            }
            return testSet;
        }

        /// <summary>
        /// Given the input file and the location to place the output file
        /// it processes generates the samples collection, processes it then 
        /// writes the results to disk.
        /// </summary>
        /// <param name="inputFile">path and name of the input file.</param>
        /// <param name="writeToLocation"></param>
        private static void ProcessSamples(string inputFile, string writeToLocation)
        {
            var testSet = LoadTests(inputFile);
            if (testSet.Count > 0)
            {
                var testResults = ExecuteTests(testSet);
                WriteResults(testResults, writeToLocation);
                WriteLine("Finished");
            }
            else
            {
                WriteLine("No samples in file.");
            }
            Environment.ExitCode = 0;
        }

        /// <summary>
        /// Executes the samples in the input file
        /// </summary>
        /// <param name="tests">A llst of samples to be scored.</param>
        /// <returns>A list of TestResults contains the scored samples.</returns>
        public static List<TestResult> ExecuteTests(List<Sample> tests)
        {
            var formulas = new List<IFormula>
            {
                new FleschKincaidReadingEase(),
                new GunningFog(),
                new ColemanLiau(),
                new AutomatedReadabilityIndex(),
                new SMOGIndex(),
                new FleschKincaidGradeLevel()
            };
            var runner = new TestRunner(tests, formulas);
            var results = runner.Execute();
            return results;
        }

        /// <summary>
        /// Writes the results to the location
        /// </summary>
        /// <param name="results">List of results to be written to file</param>
        /// <param name="location">The name of the folder to create the output folder in.</param>
        /// <remarks>
        /// Within the location a folder is created using the current date and time to stop it from 
        /// over writing an existing folder.
        /// </remarks>
        public static void WriteResults(List<TestResult> results, string location)
        {
            Clear();
            var name = DateTime.Now.ToString("ddMMyyyy-HHmmss");
            var output = Path.Combine(location, $"Output-{name}");

            WriteLine($"Writing results to {output}");
            Directory.CreateDirectory(output);
            var consoleWriter = new ConsoleStatusDisplay();

            consoleWriter.SetLine(1);
            var topLevelWriter = new SummaryCSVResults(output, consoleWriter);
            topLevelWriter.Write(results);

            consoleWriter.SetLine(3);
            var sentencesWriter = new SentencesCSVResultsWriter(output, consoleWriter);
            sentencesWriter.Write(results);

            consoleWriter.SetLine(5);
            var wordsWriter = new WordsCSVResults(output, consoleWriter);
            wordsWriter.Write(results);

            consoleWriter.SetLine(7);
            var resultsWriter = new CumulativeResultsWriter(output, consoleWriter);
            resultsWriter.Write(results);
        }
    }
}
