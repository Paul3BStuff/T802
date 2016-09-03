namespace FormulaRunner.Domain.Formulas
{
    /// <summary>
    /// Automated Readability Index
    /// </summary>
    public class AutomatedReadabilityIndex : IFormula
    {
        public string Name
        {
            get
            {
                return "ARI";
            }
        }

        public double Evaluate(string text)
        {
            var formula = TextStatistics.Net.TextStatistics.Parse(text);
            return formula.AutomatedReadabilityIndex();
        }
    }
}
