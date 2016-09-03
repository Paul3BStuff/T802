using System.Collections.Generic;

namespace FormulaRunner.Domain
{
    /// <summary>
    /// Implementers of this class write to an output device.
    /// </summary>
    public interface IResultsWriter
    {
        /// <summary>
        /// Writes a collection of result instances to be written.
        /// </summary>
        /// <param name="results">The collection to be written.</param>
        void Write(List<TestResult> results);
    }
}
