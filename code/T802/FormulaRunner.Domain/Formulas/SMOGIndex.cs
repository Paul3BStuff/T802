namespace FormulaRunner.Domain.Formulas
{
    /// <summary>
    /// SMOG Index
    /// </summary>
    public class SMOGIndex : IFormula
    {
        public string Name
        {
            get
            {
                return "SMOG";
            }
        }

        public double Evaluate(string text)
        {
            var formula = TextStatistics.Net.TextStatistics.Parse(text);
            return formula.SMOGIndex();
        }
    }
}
