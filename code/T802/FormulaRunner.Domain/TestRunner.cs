using System.Collections.Generic;

namespace FormulaRunner.Domain
{
    /// <summary>
    /// Executes samples against the collection of formulas.
    /// </summary>
    public class TestRunner
    {
        private readonly List<IFormula> _formulas;
        private readonly List<Sample> _samples;

        /// <summary>
        /// Constructs an instance with samples and formulas.
        /// </summary>
        /// <param name="samples">The samples to be processed.</param>
        /// <param name="formulas">The formulas to process the samples.</param>
        public TestRunner(IEnumerable<Sample> samples, List<IFormula> formulas)
        {
            _samples = new List<Sample>(samples);
            _formulas = new List<IFormula>(formulas);
        }

        /// <summary>
        /// Instructs the class to process the samples.
        /// </summary>
        /// <returns>The results of the samples.</returns>
        public List<TestResult> Execute()
        {
            var results = new List<TestResult>();
            foreach (var formula in _formulas)
            {
                foreach (var sample in _samples)
                {
                    var score = sample.Execute(formula);
                    var result = TestResult.Build(sample, formula.Name, score);
                    results.Add(result);
                }
            }
            return results;
        }
    }
}
