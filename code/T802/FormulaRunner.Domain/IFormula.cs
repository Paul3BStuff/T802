namespace FormulaRunner.Domain
{
    /// <summary>
    /// Interface used by classes to host a Readability formula.
    /// </summary>
    public interface IFormula
    {
        /// <summary>
        /// The name of the formula.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the score achieved by the text.
        /// </summary>
        /// <param name="text">The text to be scored.</param>
        /// <returns>The score of the text.</returns>
        double Evaluate(string text);

    }
}
